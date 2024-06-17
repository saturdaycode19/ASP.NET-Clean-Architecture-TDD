using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Application.UseCases.Authentication;
using Identity.Core.Domain.Params;
using Identity.Core.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IUserRepository repository;

        public TokenController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Token(ParamCreateToken param)
        {
            var createToken = new CreateToken(repository);
            var result = createToken.Handle(param);

            if (!string.IsNullOrWhiteSpace(result.Message)) return BadRequest(result.Message);

            return Ok(result.AccessToken);
        }
    }
}

