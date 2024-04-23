using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testApi.Dtos.Comment;
using testApi.Models;

namespace testApi.interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<List<Comment>?> GetByStockIdAsync(int id);
        Task<Comment> CreateAsync(Comment commentModel, int stockId);
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment?> UpdateAsync(int id, UpdateCommentDto updateCommentDto);
        Task<Comment?> DeleteAsync(int id);
    }
}