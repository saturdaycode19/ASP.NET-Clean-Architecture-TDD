using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Core.Domain.Params;
using Microsoft.AspNetCore.Mvc;
using Identity.Application.UseCases.Users;
using Identity.Core.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repository;

        public UserController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUserApi(ParamCreateUser user)
        {

            var createUser = new CreateUser(repository);
            var result = await createUser.Handle(user);

            if (!string.IsNullOrWhiteSpace(result.Message)) return BadRequest(result.Message);
            result.User.Password = string.Empty;
            return Ok(result.User);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserApi(Guid id)
        {
            var deleteUser = new DeleteUser(repository);
            var result = deleteUser.Handle(new ParamDeleteUser() { UserId = id });

            if (!string.IsNullOrWhiteSpace(result.Message)) return BadRequest(result.Message);

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetUserByIdApi(Guid id)
        {
            var getUserById = new GetUserById(repository);
            var result = getUserById.Handle(new ParamGetUserById() { UserId = id });

            if (!string.IsNullOrWhiteSpace(result.Message)) return BadRequest(result.Message);

            result.User.Password = string.Empty;
            return Ok(result.User);
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var getUsers = new GetUsers(repository);
            var result = getUsers.Handle(new ParamGetUsers());

            return Ok(result.Users);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserApi(ParamUpdateUser param)
        {
            var updateUser = new UpdateUser(repository);
            var result = await updateUser.Handle(param);

            if (!string.IsNullOrWhiteSpace(result.Message)) return BadRequest(result.Message);

            result.User.Password = string.Empty;
            return Ok(result.User);
        }
    }
}

