using ComicBookStoreAPI.Data;
using ComicBookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        public readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult<TResult>> GetById<TResult>(string id, Expression<Func<ApplicationUser, TResult>> selector)
        {
            var applicationUser = await _db.ApplicationUser
                .Where(a => a.Id == id)
                .Select(selector)
                .FirstOrDefaultAsync();

            return applicationUser;
        }
    }
}
