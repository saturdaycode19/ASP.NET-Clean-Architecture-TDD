using System;
using Identity.Core.Domain.Params;
using Identity.Core.Domain.Repositories;
using Identity.Core.Domain.Responses;

namespace Identity.Application.UseCases.Users
{
	public class DeleteUser
	{

		public readonly IUserRepository repository;

		public DeleteUser(IUserRepository repository)
		{
			this.repository = repository;
		}

		public ResponseDeleteUser Handle(ParamDeleteUser param)
		{
			var response = new ResponseDeleteUser();

			var validation = new ParamDeleteUserValidation();
			var resultValidation = validation.Validate(param);

			if (!resultValidation.IsValid)
			{
				foreach (var error in resultValidation.Errors)
				{
					response.Message += error.ErrorMessage + Environment.NewLine;
				}
			}

			if (!string.IsNullOrWhiteSpace(response.Message)) return response;

			var user = repository.GetUserById(param.UserId);
			if (user == null)
			{
				response.Message = "User tidak ditemukan";
				return response;
			}

			repository.Delete(param.UserId);
			repository.Commit();

			return response;
		}
	}
}

