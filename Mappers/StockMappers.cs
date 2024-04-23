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
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStockFromCreateStockDto(this CreateStockDto stockDto){
            return new Stock 
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                LastDiv = stockDto.LastDiv,
                Purchase = stockDto.Purchase,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }

        public static Stock ToStockFromUpdateStockDto(this UpdateStockDto updateStockDto){
            return new Stock 
            {
                LastDiv = updateStockDto.LastDiv,
                Purchase = updateStockDto.Purchase,
                Industry = updateStockDto.Industry,
                MarketCap = updateStockDto.MarketCap
            };
        }
    }
}