using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.DatasetService
{
    public interface IDatasetService
    {
        Task<IEnumerable<Dataset>> GetAllDatasetAsync(string label, int limit = 50);
        Task<IEnumerable<Dataset>> GetAllTestDatasetAsync(string label, int limit = 50);
        Task<IEnumerable<Dataset>> GetAllTrainDatasetAsync(string label, int limit = 50);
        Task<Dataset> GetDatasetAsync(Guid id);
        Task InsertIntoDataset(Dataset dataset);
        Task UpdateDataset(Dataset dataset);
        Task DeleteFromDatasetAsync(Guid id);
    }
}
