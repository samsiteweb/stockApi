using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testApi.Models;

namespace testApi.interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
    }
}