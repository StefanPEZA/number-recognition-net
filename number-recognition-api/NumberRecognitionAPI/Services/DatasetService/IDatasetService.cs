using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.DatasetService
{
    public interface IDatasetService
    {
        Task<IEnumerable<Dataset>> GetAllDatasetAsync(int label, int limit = 10);
        Task<IEnumerable<Dataset>> GetAllTestDatasetAsync(int label, int limit = 10);
        Task<IEnumerable<Dataset>> GetAllTrainDatasetAsync(int label, int limit = 10);
        Task<Dataset> GetDatasetAsync(Guid id);
        Task InsertIntoDataset(Dataset dataset);
        Task UpdateDataset(Dataset dataset);
        Task DeleteFromDatasetAsync(Guid id);
    }
}
