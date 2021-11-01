using ComicBookStoreAPI.Repositories;
using ComicBookStoreAPI.Repositories.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Services
{
    public class ApplicationUserService
    {
        public readonly IApplicationUserRepository _applicationUserRepository;

        public ApplicationUserService(IApplicationUserRepository applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;

        }

        // GET: Get Application User By Id
        public async Task<ActionResult<ApplicationUserDTO>> GetApplicationUserDTO(string id)
        {
            return await _applicationUserRepository.GetById(id, ApplicationUserDTO.ApplicationUserSelector);
        }
    }
}
