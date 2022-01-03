using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace NumberRecognitionML
{
    /// <summary>
    /// The Digit class represents one mnist digit.
    /// </summary>
    public class Digit
    {
        public float Number { get; set; }
        [VectorType(784)] public float[] PixelValues { get; set; }
    }

    /// <summary>
    /// The DigitPrediction class represents one digit prediction.
    /// </summary>
    public class DigitPrediction
    {
        [ColumnName("Score")]
        public float[] Score { get; set; }
        public float PredictedNumber { get; set; }
    }

    public class NumberRecognition
    {
        private static bool HasHeaders { get; set; } = true;
        public void Train(string dataPath, string modelPath,string testPath)
        {
            // create a machine learning context
            var context = new MLContext();

            // load data
            Trace.WriteLine("Loading data....");
            var dataView = context.Data.LoadFromTextFile(
                path: dataPath,
                columns: new[]
                {
                    new TextLoader.Column("Number", DataKind.Single, 0),
                    new TextLoader.Column(nameof(Digit.PixelValues), DataKind.Single, 1, 784)
                },
                hasHeader: HasHeaders,
                separatorChar: ',');
            var testSet = context.Data.LoadFromTextFile(
                path: testPath,
                columns: new[]
                {
                    new TextLoader.Column("Number", DataKind.Single, 0),
                    new TextLoader.Column(nameof(Digit.PixelValues), DataKind.Single, 1, 784)
                },
                hasHeader: HasHeaders,
                separatorChar: ',');

            // split data into a training and test set

            // build a training pipeline
            // step 1: concatenate all feature columns
            var pipeline = context.Transforms.Concatenate("Features",
                //DefaultColumnNames.Features,
                nameof(Digit.PixelValues))

                .Append(context.Transforms.Conversion.MapValueToKey(inputColumnName: "Number", outputColumnName: "Label"))

                // step 2: cache data to speed up training                
                .AppendCacheCheckpoint(context)

                // step 3: train the model with SDCA
                .Append(context.MulticlassClassification.Trainers.LightGbm(
                    labelColumnName: "Label",
                    featureColumnName: "Features"))

                .Append(context.Transforms.Conversion.MapKeyToValue("PredictedNumber", "PredictedLabel"));

            // train the model
            Trace.WriteLine("Training model....");
            var model = pipeline.Fit(dataView);


            // use the model to make predictions on the test data
            Trace.WriteLine("Evaluating model....");
            var predictions = model.Transform(testSet);

            // evaluate the predictions
            var metrics = context.MulticlassClassification.Evaluate(
                data: predictions
                //label: "Number",
                //score: "Score" /*DefaultColumnNames.Score*/
                );

            // show evaluation metrics
            Trace.WriteLine($"Evaluation metrics");
            Trace.WriteLine($"    MicroAccuracy:    {metrics.MicroAccuracy:0.###}");
            Trace.WriteLine($"    MacroAccuracy:    {metrics.MacroAccuracy:0.###}");
            Trace.WriteLine($"    LogLoss:          {metrics.LogLoss:#.###}");
            Trace.WriteLine($"    LogLossReduction: {metrics.LogLossReduction:#.###}");

            context.Model.Save(model, dataView.Schema, modelPath);
            Trace.WriteLine($"Model {modelPath} saved.");

            _predictionEngine = context.Model.CreatePredictionEngine<Digit, DigitPrediction>(model);
        }

        private PredictionEngine<Digit, DigitPrediction> _predictionEngine;

        public void LoadModel(string modelPath)
        {
            var context = new MLContext();
            DataViewSchema schema;
            var model = context.Model.Load(modelPath, out schema);
            _predictionEngine = context.Model.CreatePredictionEngine<Digit, DigitPrediction>(model);
            Trace.WriteLine($"Model {modelPath} loaded.");
        }

        public DigitPrediction PredictDigit(Digit digit)
        {
            try
            {
                return _predictionEngine.Predict(digit);
            }
            catch (Exception exp)
            {
                Trace.WriteLine("Prediction failed. Model loaded?" + exp.Message);
                return null;
            }
        }

        public float Predict(IEnumerable<float> attributes)
        {
            Digit digit = new Digit();
            digit.PixelValues = attributes.ToArray();
 /*           for (int i = 0; i < 784; i++)
            {
                if (digit.PixelValues[i] > 1f)
                    digit.PixelValues[i] = 1f;
            }*/
            //Train(@".\..\Services\ImageService\data\tempFileTrain.csv", @".\..\Services\ImageService\data\model", @".\..\Services\ImageService\data\tempFileTest.csv");
            LoadModel(@".\..\Services\ImageService\data\model");
            var predictionResult = PredictDigit(digit);
            return predictionResult.PredictedNumber;
        }
    }
}