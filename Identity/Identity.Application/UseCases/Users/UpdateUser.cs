using System;
using Identity.Core.Domain.Params;
using Identity.Core.Domain.Repositories;
using Identity.Core.Domain.Responses;
using Identity.Core.Entities;

namespace Identity.Application.UseCases.Users
{
	public class UpdateUser
	{
		private readonly IUserRepository repository;

		public UpdateUser(IUserRepository repository)
		{
			this.repository = repository;
		}

		public async Task<ResponseUpdateUser> Handle(ParamUpdateUser param)
		{
			var response = new ResponseUpdateUser();

			var user = repository.GetUserById(param.UserId);
			if (user == null)
			{
				response.Message = "User tidak ditemukan";
				return response;
			}

			var emailExists = repository.GetUserByEmailAndNotById(param.Email, param.UserId);
			if (emailExists != null)
			{
				response.Message = "Email sudah digunakan";
				return response;
			}

			var result = repository.UpdateUser(param.UserId, new User()
			{
				UserId = param.UserId,
				Email = param.Email,
				 FullName = param.FullName,
				 Password = param.Password
			});

			await repository.Commit();

			response.User = result;
			return response;
        }
	}
}

