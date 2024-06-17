using System;
using Identity.Core.Domain.Params;
using Identity.Core.Domain.Repositories;
using Identity.Core.Domain.Responses;
using Identity.Core.Entities;

namespace Identity.Application.UseCases.Users
{
	public class CreateUser
	{
		private readonly IUserRepository repository;


		public CreateUser(IUserRepository repository)
		{
			this.repository = repository;

        }

		public async Task<ResponseCreateUser> Handle(ParamCreateUser param)
		{
			var response = new ResponseCreateUser();

			var validation = new ParamCreateUserValidator();
			var valResult = validation.Validate(param);

            if (!valResult.IsValid)
            {
                foreach (var failure in valResult.Errors)
                {
					response.Message += failure.ErrorMessage + Environment.NewLine;
                }
            }

			if (!string.IsNullOrWhiteSpace(response.Message)) return response;

            var user = repository.GetUserByEmail(param.Email);
			if (user != null)
			{
				response.Message = "Email sudah digunakan";
				return response;
            }

			user = await repository.CreateUser(new User()
			{
				Email = param.Email,
				FullName = param.FullName,
				Password = param.Password
			});

			await repository.Commit();

			response.User = user;

			return response;
		}
	}
}

