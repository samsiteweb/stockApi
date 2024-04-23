using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testApi.interfaces;
using testApi.Mappers;

namespace testApi.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController: ControllerBase
    {
        ICommentRepository _commentRepo;
        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        public async Task<IActionResult> GetAll(){
        var allComments = await  _commentRepo.GetAllAsync();
        var comments = allComments.Select(c => c.ToCommentDto());

        return Ok(comments);
       }

    }
}