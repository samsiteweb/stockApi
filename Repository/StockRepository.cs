using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using testApi.Data;
using testApi.Dtos.Stock;
using testApi.interfaces;
using testApi.Models;

namespace testApi.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteStock(int id)
        {
            var existingStock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            
            if (existingStock == null)
            {
                return null;                
            };

            _context.Stock.Remove(existingStock);
            await _context.SaveChangesAsync();

            return existingStock;
        }

        public async Task<Stock?> GetStockByIdAsync(int id)
        {   
            var stock = await _context.Stock.FindAsync(id);

            if (stock == null){
                return null;
            }
            return stock;
        }

        public async Task<Stock?> UpdateStock(int id, UpdateStockDto stockDto)
        {
              var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel == null)
            {
                return null;                
            };

            stockModel.Purchase = stockDto.Purchase;
            stockModel.LastDiv = stockDto.LastDiv;
            stockModel.Industry = stockDto.Industry;
            stockModel.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
             return await _context.Stock.ToListAsync();
        }
    }
}