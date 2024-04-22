using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testApi.Dtos.Stock;
using testApi.Models;

namespace testApi.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel){
            return new StockDto 
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                LastDiv = stockModel.LastDiv,
                Purchase = stockModel.Purchase,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap
            };
        }
    }
}