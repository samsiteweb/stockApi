using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApi.Data;
using testApi.Dtos.Stock;
using testApi.Mappers;

namespace testApi.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController: ControllerBase
    {
        private readonly ApplicationDBContext _context;
       public StockController(ApplicationDBContext context)
       {
        _context = context;
       }

       [HttpGet]
       public async Task<IActionResult> GetAll(){
            var allStocks = await _context.Stock.ToListAsync();
            var stocks = allStocks.Select(s => s.ToStockDto());

            return Ok(stocks);

       }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            var stock = await _context.Stock.FindAsync(id);

            if (stock == null){
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto){
            var stockModel = stockDto.ToStockFromCreateStockDto();
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updateStock){
            var existingStock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if (existingStock == null)
            {
                return NotFound();                
            };

            existingStock.Purchase = updateStock.Purchase;
            existingStock.LastDiv = updateStock.LastDiv;
            existingStock.Industry = updateStock.Industry;
            existingStock.MarketCap = updateStock.MarketCap;

            await _context.SaveChangesAsync();

            return Ok(existingStock.ToStockDto());

        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> Delete([FromRoute] int id){
            var existingStock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            Console.WriteLine(id);

            if (existingStock == null)
            {
                return NotFound();                
            };

            _context.Stock.Remove(existingStock);
            await _context.SaveChangesAsync();

            return NoContent();

         }
    }
}