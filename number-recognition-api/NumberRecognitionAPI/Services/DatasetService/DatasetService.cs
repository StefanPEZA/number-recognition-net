using Domain.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.DatasetService
{
    public class DatasetService : IDatasetService
    {
        private readonly IRepository<Dataset> _repository;
        public DatasetService(IRepository<Dataset> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Dataset>> GetAllDatasetAsync(int label, int limit = 10)
        {
            return await _repository.GetAllAsync(limit, d => d.Label == label);
        }

        public async Task<IEnumerable<Dataset>> GetAllTestDatasetAsync(int label, int limit = 10)
        {
            return await _repository.GetAllAsync(limit, d => d.IsTest && d.Label == label);
        }

        public async Task<IEnumerable<Dataset>> GetAllTrainDatasetAsync(int label, int limit = 10)
        {
            return await _repository.GetAllAsync(limit, d => !d.IsTest && d.Label == label);
        }

        public async Task<Dataset> GetDatasetAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task InsertIntoDataset(Dataset dataset)
        {
            await _repository.InsertAsync(dataset);
        }

        public async Task UpdateDataset(Dataset dataset)
        {
            await _repository.UpdateAsync(dataset);
        }

        public async Task DeleteFromDatasetAsync(Guid id)
        {
            Dataset dataset = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(dataset);
        }
    }
}
