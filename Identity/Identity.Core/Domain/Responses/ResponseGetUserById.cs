using System;
using Identity.Core.Entities;

namespace Identity.Core.Domain.Responses
{
	public class ResponseGetUserById
	{
		public User User { get; set; }
		public string Message { get; set; }
	}
}

