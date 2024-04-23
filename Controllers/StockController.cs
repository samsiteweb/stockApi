using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApi.Data;
using testApi.Dtos.Stock;
using testApi.interfaces;
using testApi.Mappers;

namespace testApi.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController: ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
       public StockController(ApplicationDBContext context, IStockRepository stockRepo)
       {
        _stockRepo = stockRepo;
        _context = context;
       }

       [HttpGet]
       public async Task<IActionResult> GetAll(){
            var allStocks = await _stockRepo.GetAllAsync();
            var stocks = allStocks.Select(s => s.ToStockDto());

            return Ok(stocks);

       }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            var stock = await _stockRepo.GetStockByIdAsync(id);

            return Ok(stock?.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto){
            var stockModel = stockDto.ToStockFromCreateStockDto();
            await _stockRepo.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updateStock){
            var updatedStock = await _stockRepo.UpdateStock(id, updateStock);

            return Ok(updatedStock?.ToStockDto());

        }

        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> Delete([FromRoute] int id){
            await _stockRepo.DeleteStock(id);

            return NoContent();

         }
    }
}