using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testApi.Dtos.Comment;
using testApi.interfaces;
using testApi.Mappers;

namespace testApi.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController: ControllerBase
    {
        ICommentRepository _commentRepo;
        IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
        var allComments = await  _commentRepo.GetAllAsync();
        var comments = allComments.Select(c => c.ToCommentDto());

        return Ok(comments);
       }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create( [FromBody] CreateCommentDto createCommentDto, [FromRoute] int stockId){
            var stockExist = await _stockRepo.StockExist(stockId);

            if (!stockExist)
            {
                return BadRequest("Stock with the supplied Id was not found");
            };
            var commentDto = createCommentDto.ToCommentFromCreateCommentDto(stockId);
            var comment = await _commentRepo.CreateAsync(commentDto, stockId);
         
            return CreatedAtAction(nameof(GetById), new {id = comment.Id}, comment.ToCommentDto());
        }

        [HttpGet]
        [Route("stockComments/{stockId:int}")]
        public async Task<IActionResult> GetByStockId([FromRoute] int stockId){
            var allStockComments = await _commentRepo.GetByStockIdAsync(stockId);
            var comments = allStockComments?.Select(c => c.ToCommentDto());

            return Ok(comments);
        }
        

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            var comments = await _commentRepo.GetByIdAsync(id);

            if (comments == null)
            {
                return NotFound();
            }

            return Ok(comments);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto updateCommentDto){
            var comment = await _commentRepo.UpdateAsync(id, updateCommentDto);
         
            return Ok(comment?.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            await _commentRepo.DeleteAsync(id);

            return NoContent();
        }

    }
}