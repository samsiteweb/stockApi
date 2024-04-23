using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using testApi.Data;
using testApi.Dtos.Comment;
using testApi.interfaces;
using testApi.Models;

namespace testApi.Repository
{
    public class CommentRepository : ICommentRepository
    {
        ApplicationDBContext  _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel, int stockId)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();

            return commentModel;

        }


        public async Task<Comment?> DeleteAsync(int id)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            
            if (existingComment == null)
            {
                return null;
            }
             _context.Comments.Remove(existingComment);
             await _context.SaveChangesAsync();

             return existingComment;

        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
             var comment = await _context.Comments.FindAsync(id);

             if (comment == null)
             {
                return null;
             }

             return comment;

        }

        public async Task<List<Comment>?> GetByStockIdAsync(int stockId)
        {
           var comments = await _context.Comments.Where(c => c.StockId == stockId).ToListAsync();

           if (comments == null || !comments.Any())
           {
            return null;
           }

           return comments;
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentDto updateCommentDto)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            
            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = updateCommentDto.Title;
            existingComment.Content = updateCommentDto.Content;

            await _context.SaveChangesAsync();

            return existingComment;
        }
    }
}