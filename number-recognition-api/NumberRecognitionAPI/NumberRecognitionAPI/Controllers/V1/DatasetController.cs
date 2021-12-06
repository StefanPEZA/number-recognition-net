using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DatasetService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NumberRecognitionAPI.Controllers.V1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/dataset")]
    [ApiController]
    public class DatasetController : ControllerBase
    {
        private readonly IDatasetService _datasetService;

        public DatasetController(IDatasetService datasetService)
        {
            _datasetService = datasetService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDatasetWithId([FromRoute] Guid id)
        {
            object response;
            var datasetEntity = await _datasetService.GetDatasetAsync(id);
            if (datasetEntity == null)
            {
                response = new
                {
                    status = "ERROR",
                    message = "No dataset entry was found with id: " + id.ToString()
                };
                return BadRequest(response);
            }
            return Ok(datasetEntity);
        }

        [HttpGet("all/{label}")]
        public async Task<IActionResult> GetAllDataset(string label, [FromQuery] int limit = 50)
        {
            object response;
            List<Dataset> dataset = (List<Dataset>)await _datasetService.GetAllDatasetAsync(label, limit);
            if (dataset == null || dataset.Count <= 0)
            {
                response = new
                {
                    status = "ERROR",
                    message = "No dataset was found for label: " + label.ToString()
                };
                return BadRequest(response);
            }
            return Ok(dataset);
        }

        [HttpGet("train/{label}")]
        public async Task<IActionResult> GetAllTrainDataset(string label, [FromQuery] int limit = 50)
        {
            object response;
            List<Dataset> dataset = (List<Dataset>)await _datasetService.GetAllTrainDatasetAsync(label, limit);
            if (dataset == null || dataset.Count <= 0)
            {
                response = new
                {
                    status = "ERROR",
                    message = "No train dataset was found for label: " + label.ToString()
                };
                return BadRequest(response);
            }
            return Ok(dataset);
        }

        [HttpGet("test/{label}")]
        public async Task<IActionResult> GetAllTestDataset(string label, [FromQuery] int limit = 50)
        {
            object response;
            List<Dataset> dataset = (List<Dataset>)await _datasetService.GetAllTestDatasetAsync(label, limit);
            if (dataset == null || dataset.Count <= 0)
            {
                response = new
                {
                    status = "ERROR",
                    message = "No test dataset was found for label: " + label.ToString()
                };
                return BadRequest(response);
            }
            return Ok(dataset);
        }

        private async Task<Dataset> AddToDataset(bool isTest, string label, byte[] image_bytes)
        {
            Dataset dataset = new Dataset()
            {
                Label = label,
                ImageMatrix = image_bytes,
                IsTest = isTest
            };
            await _datasetService.InsertIntoDataset(dataset);
            return dataset;
        }

        [HttpPost("test/{label}")]
        public async Task<IActionResult> AddTestDataset(string label, IFormFile image)
        {
            byte[] image_bytes = new byte[image.Length];
            await image.OpenReadStream().ReadAsync(image_bytes, 0, (int) image.Length);
            Dataset dataset = await AddToDataset(true, label, image_bytes);
            return Ok(dataset);
        }

        [HttpPost("train/{label}")]
        public async Task<IActionResult> AddTrainDataset(string label, IFormFile image)
        {
            byte[] image_bytes = new byte[image.Length];
            await image.OpenReadStream().ReadAsync(image_bytes, 0, (int)image.Length);
            Dataset dataset = await AddToDataset(false, label, image_bytes);
            return Ok(dataset);
        }
    }
}
