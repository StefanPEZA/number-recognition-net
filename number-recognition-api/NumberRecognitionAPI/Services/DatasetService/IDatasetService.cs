using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.DatasetService
{
    public interface IDatasetService
    {
        Task<IEnumerable<Dataset>> GetAllTestDatasetAsync();
        Task<IEnumerable<Dataset>> GetAllTrainDatasetAsync();
        Task<Dataset> GetDatasetAsync(Guid id);
        void InsertIntoDataset(Dataset dataset);
        void UpdateDataset(Dataset dataset);
        void DeleteFromDatasetAsync(Guid id);
    }
}
