using ComicBookStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories.DTO
{
    public class ApplicationUserDTO
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static Expression<Func<ApplicationUser, ApplicationUserDTO>> ApplicationUserSelector
        {
            get
            {
                return applicationUser => new ApplicationUserDTO()
                {
                    ID = applicationUser.Id,
                    UserName = applicationUser.UserName,
                    FirstName = applicationUser.FirstName,
                    LastName = applicationUser.LastName
                };
            }
        }
    }
}
