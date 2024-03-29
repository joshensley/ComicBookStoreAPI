﻿using ComicBookStoreAPI.Models;
using ComicBookStoreAPI.Repositories.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories
{
    public interface ICartRepository
    {
        Task<ActionResult<TResult>> GetProductFromApplicationUserCart<TResult>(Expression<Func<Cart, TResult>> selector, string applicationUserId, int productID);

        Task<ActionResult<Cart>> GetCartProductByIdReturnCartModel(int id);

        Task<ActionResult<IEnumerable<TResult>>> GetByApplicationUserId<TResult>(Expression<Func<Cart, TResult>> selector, string applicationUserId);

        Task<ActionResult<List<TResult>>> GetByApplicationUserDetailsId<TResult>(Expression<Func<Cart, TResult>> selector, string applicationUserId);

        Task<ActionResult<Cart>> Post(string applicationUserId, int productId);

        Task<ActionResult<Cart>> Edit(Cart cart);

        Task<ActionResult<int>> Delete(Cart product);
    }
}
