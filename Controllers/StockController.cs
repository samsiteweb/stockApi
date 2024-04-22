using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
       public IActionResult GetAll(){
            var stocks = _context.Stock.ToList().Select(s => s.ToStockDto());

            return Ok(stocks);

       }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id){
            var stock = _context.Stock.Find(id);

            if (stock == null){
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockDto stockDto){
            var stockModel = stockDto.ToStockFromCreateStockDto();
            _context.Stock.Add(stockModel);
            _context.SaveChanges();
            
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto());
        }

    }
}