using System;
using Identity.Core.Entities;

namespace Identity.Core.Domain.Responses
{
	public class ResponseCreateUser
	{
		public User User { get; set; }
		public string Message { get; set; }
	}
}

