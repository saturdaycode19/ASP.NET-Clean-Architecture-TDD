using System;
using Identity.Core.Entities;

namespace Identity.Core.Domain.Responses
{
	public class ResponseUpdateUser
	{
		public string Message { get; set; }
		public User User { get; set; }
	}
}

