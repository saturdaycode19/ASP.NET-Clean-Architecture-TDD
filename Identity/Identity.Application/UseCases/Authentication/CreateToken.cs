using System;
using Identity.Core.Domain.Params;
using Identity.Core.Domain.Repositories;
using Identity.Core.Domain.Responses;

namespace Identity.Application.UseCases.Authentication
{
	public class CreateToken
	{

		private readonly IUserRepository repository;

		public CreateToken(IUserRepository repository)
		{
			this.repository = repository;
		}

		public ResponseCreateToken Handle(ParamCreateToken param)
		{
			var response = new ResponseCreateToken();

			var validation = new ParamCreateTokenValidation();
			var resultValidate = validation.Validate(param);

			if (!resultValidate.IsValid)
			{
				foreach (var error in resultValidate.Errors)
				{
					response.Message += error.ErrorMessage + Environment.NewLine;
				}
			}

			if (!string.IsNullOrWhiteSpace(response.Message)) return response;

			var result = repository.Authenticate(param.Email, param.Password);
			if (result == null)
			{
				response.Message = "User tidak ditemukan";
				return response;
			}

			response.AccessToken = result;
			return response;
		}
	}
}

