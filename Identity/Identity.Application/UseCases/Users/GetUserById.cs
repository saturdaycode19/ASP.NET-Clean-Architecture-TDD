using System;
using Identity.Core.Domain.Params;
using Identity.Core.Domain.Repositories;
using Identity.Core.Domain.Responses;

namespace Identity.Application.UseCases.Users
{
	public class GetUserById
	{
		private readonly IUserRepository repository;

		public GetUserById(IUserRepository repository)
		{
			this.repository = repository;
		}


		public ResponseGetUserById Handle(ParamGetUserById param)
		{
			var response = new ResponseGetUserById();

			var result = repository.GetUserById(param.UserId);
			if (result == null)
			{
				response.Message = "User tidak ditemukan";
				return response;
			}

			response.User = result;
			return response;

        }
	}
}

