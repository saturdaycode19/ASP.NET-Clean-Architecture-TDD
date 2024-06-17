using System;
using Identity.Core.Entities;

namespace Identity.Core.Domain.Responses
{
	public class ResponseGetUsers
	{
		public List<User> Users { get; set; }
		public string Message { get; set; }
	}
}

