using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using testApi.Data;
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
        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }
    }
}