using System;
using Identity.Core.Domain.Params;
using Identity.Core.Domain.Repositories;
using Identity.Core.Domain.Responses;

namespace Identity.Application.UseCases.Users
{
	public class GetUsers
	{
		private readonly IUserRepository repository;

		public GetUsers(IUserRepository repository)
		{
			this.repository = repository;
		}


		public ResponseGetUsers Handle(ParamGetUsers param)
		{
			var result = repository.GetUsers();
			return new ResponseGetUsers()
			{
				Users = result
			};
		}
	}
}

