using ComicBookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories
{
    public interface IApplicationUserRepository
    {
        Task<ActionResult<TResult>> GetById<TResult>(string id, Expression<Func<ApplicationUser, TResult>> selector);
    }
}
