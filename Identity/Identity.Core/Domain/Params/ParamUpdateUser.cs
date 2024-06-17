using System;
using Identity.Core.Entities;

namespace Identity.Core.Domain.Params
{
	public class ParamUpdateUser
	{
		public Guid UserId { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}

