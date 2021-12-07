using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Repository;
using Services.DatasetService;
using ServicesTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Assert.AreEqual(datasetList1, datasetList2);
        }

        [TestMethod()]
        public void GetAllTestDatasetAsyncTest()
        {
            List<Dataset> datasetList1 = (List<Dataset>)_datasetService.GetAllTestDatasetAsync("0").Result;
            List<Dataset> datasetList2 = (List<Dataset>)_datasetService.GetAllTestDatasetAsync("0").Result;
            Assert.AreEqual(datasetList1, datasetList2);
        }

        [TestMethod()]
        public void GetAllTrainDatasetAsyncTest()
        {
            List<Dataset> datasetList1 = (List<Dataset>)_datasetService.GetAllTrainDatasetAsync("0").Result;
            List<Dataset> datasetList2 = (List<Dataset>)_datasetService.GetAllTrainDatasetAsync("0").Result;
            Assert.AreEqual(datasetList1, datasetList2);
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
                Label = "0",
                ImageMatrix = new byte[1] { (byte)0 },
                IsTest = true
            };
            Dataset dataset2 = _datasetService.GetDatasetAsync(Guid.Parse("7c13d9e7-886a-4500-80cd-89a5dd8ecc41")).Result;
            Assert.AreEqual(dataset1, dataset2);
        }

        [TestMethod()]
        public void UpdateDatasetTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteFromDatasetAsyncTest()
        {
            Assert.Fail();
        }
    }
}