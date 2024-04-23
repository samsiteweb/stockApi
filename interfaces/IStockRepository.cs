using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testApi.Dtos.Stock;
using testApi.Models;

namespace testApi.interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetStockByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateStock(int id, UpdateStockDto stockDto);
        Task<Stock?> DeleteStock(int id);
        Task<Boolean> StockExist(int id);
    }
}