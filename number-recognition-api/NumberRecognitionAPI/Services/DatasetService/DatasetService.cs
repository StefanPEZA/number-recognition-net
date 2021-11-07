using Domain.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IEnumerable<Dataset>> GetAllTestDatasetAsync()
        {
            return await _repository.GetAllAsync(d => d.IsTest);
        }

        public async Task<IEnumerable<Dataset>> GetAllTrainDatasetAsync()
        {
            return await _repository.GetAllAsync(d => !d.IsTest);
        }

        public async Task<Dataset> GetDatasetAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public void InsertIntoDataset(Dataset dataset)
        {
            _repository.InsertAsync(dataset);
        }

        public void UpdateDataset(Dataset dataset)
        {
            _repository.UpdateAsync(dataset);
        }

        public async void DeleteFromDatasetAsync(Guid id)
        {
            Dataset dataset = await _repository.GetByIdAsync(id);
            _repository.DeleteAsync(dataset);
        }
    }
}
