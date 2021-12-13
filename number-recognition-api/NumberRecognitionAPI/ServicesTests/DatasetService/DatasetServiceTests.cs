using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServicesTests;
using System;
using System.Collections.Generic;

namespace Services.DatasetService.Tests
{
    [TestClass()]
    public class DatasetServiceTests
    {
        private readonly IDatasetService _datasetService;

        public DatasetServiceTests()
        {
            _datasetService = Helper.GetRequiredService<IDatasetService>() ?? throw new ArgumentNullException(nameof(IDatasetService));
        }

        [TestMethod()]
        public void GetAllDatasetAsyncTest()
        {
            List<Dataset> datasetList1 = (List<Dataset>)_datasetService.GetAllDatasetAsync("0").Result;
            List<Dataset> datasetList2 = (List<Dataset>)_datasetService.GetAllDatasetAsync("0").Result;
            Assert.AreEqual(datasetList1.Count, datasetList2.Count);
        }

        [TestMethod()]
        public void GetAllTestDatasetAsyncTest()
        {
            List<Dataset> datasetList1 = (List<Dataset>)_datasetService.GetAllTestDatasetAsync("0").Result;
            List<Dataset> datasetList2 = (List<Dataset>)_datasetService.GetAllTestDatasetAsync("0").Result;
            Assert.AreEqual(datasetList1.Count, datasetList2.Count);
        }

        [TestMethod()]
        public void GetAllTrainDatasetAsyncTest()
        {
            List<Dataset> datasetList1 = (List<Dataset>)_datasetService.GetAllTrainDatasetAsync("0").Result;
            List<Dataset> datasetList2 = (List<Dataset>)_datasetService.GetAllTrainDatasetAsync("0").Result;
            Assert.AreEqual(datasetList1.Count, datasetList2.Count);
        }

        [TestMethod()]
        public void GetDatasetAsyncTest()
        {
            Dataset dataset1 = _datasetService.GetDatasetAsync(Guid.Parse("7c13d9e7-886a-4500-80cd-89a5dd8ecc41")).Result;
            Dataset dataset2 = _datasetService.GetDatasetAsync(Guid.Parse("7c13d9e7-886a-4500-80cd-89a5dd8ecc41")).Result;
            Assert.AreEqual(dataset1, dataset2);
        }

        [TestMethod()]
        public void InsertIntoDatasetTest()
        {
            Dataset dataset1 = _datasetService.GetDatasetAsync(Guid.Parse("7c13d9e7-886a-4500-80cd-89a5dd8ecc41")).Result;
            Dataset dataset = new Dataset
            {
                Id = Guid.Parse("7c13d9e7-886a-4500-80cd-89a5dd8ecc41"),
                Label = "0",
                ImageMatrix = new byte[1] { (byte)0 },
                IsTest = true
            };
            _ = _datasetService.InsertIntoDataset(dataset).Result;
            Dataset dataset2 = _datasetService.GetDatasetAsync(Guid.Parse("7c13d9e7-886a-4500-80cd-89a5dd8ecc41")).Result;
            _ = _datasetService.DeleteFromDatasetAsync(dataset2.Id);
            Assert.AreNotEqual(dataset2, dataset1);
        }

        [TestMethod()]
        public void UpdateDatasetTest()
        {
            Guid id = Guid.Parse("7c13d9e7-886a-4500-80cd-89a5dd8ecc42");
            Dataset dataset = _datasetService.GetDatasetAsync(id).Result;
            if (dataset == null)
            {
                dataset = new Dataset
                {
                    Id = id,
                    Label = "0",
                    ImageMatrix = new byte[1] { (byte)0 },
                    IsTest = true
                };
                _ = _datasetService.InsertIntoDataset(dataset).Result;
            }

            Dataset toUpdate = _datasetService.GetDatasetAsync(id).Result;
            Assert.IsNotNull(toUpdate);

            toUpdate.Label = "1";
            _ = _datasetService.UpdateDataset(toUpdate).Result;

            dataset = _datasetService.GetDatasetAsync(id).Result;
            _ = _datasetService.DeleteFromDatasetAsync(id);
            Assert.AreEqual("1", dataset.Label);
        }

        [TestMethod()]
        public void DeleteFromDatasetAsyncTest()
        {
            Guid id = Guid.Parse("7c13d9e7-886a-4500-80cd-89a5dd8ecc43");
            Dataset dataset = _datasetService.GetDatasetAsync(id).Result;
            if (dataset == null)
            {
                dataset = new Dataset
                {
                    Id = id,
                    Label = "0",
                    ImageMatrix = new byte[1] { (byte)0 },
                    IsTest = true
                };
                _ = _datasetService.InsertIntoDataset(dataset).Result;
            }

            Dataset inserted = _datasetService.GetDatasetAsync(id).Result;
            Assert.IsNotNull(inserted);

            _ = _datasetService.DeleteFromDatasetAsync(id);

            Dataset datasetDeleted = _datasetService.GetDatasetAsync(id).Result;

            Assert.IsNull(datasetDeleted);
        }
    }
}