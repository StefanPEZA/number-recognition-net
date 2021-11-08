using Domain.Models;
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
        public async Task<IActionResult> GetAllDataset(int label, [FromQuery] int limit = 10)
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
        public async Task<IActionResult> GetAllTrainDataset(int label, [FromQuery] int limit = 10)
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
        public async Task<IActionResult> GetAllTestDataset(int label, [FromQuery] int limit = 10)
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
    }
}
