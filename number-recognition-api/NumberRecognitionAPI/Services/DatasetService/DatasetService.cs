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

        public async Task<IEnumerable<Dataset>> GetAllDatasetAsync(string label, int limit = 50)
        {
            return await _repository.GetAllAsync(limit, d => d.Label == label);
        }

        public async Task<IEnumerable<Dataset>> GetAllTestDatasetAsync(string label, int limit = 50)
        {
            return await _repository.GetAllAsync(limit, d => (bool)d.IsTest && d.Label == label);
        }

        public async Task<IEnumerable<Dataset>> GetAllTrainDatasetAsync(string label, int limit = 50)
        {
            return await _repository.GetAllAsync(limit, d => !(bool)d.IsTest && d.Label == label);
        }

        public async Task<Dataset> GetDatasetAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task InsertIntoDataset(Dataset dataset)
        {
            try
            {
                await _repository.InsertAsync(dataset);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task UpdateDataset(Dataset dataset)
        {
            try
            {
                await _repository.UpdateAsync(dataset);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task DeleteFromDatasetAsync(Guid id)
        {
            try
            {
                Dataset dataset = await _repository.GetByIdAsync(id);
                await _repository.DeleteAsync(dataset);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
