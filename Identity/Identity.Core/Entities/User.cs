using System;
namespace Identity.Core.Entities
{
	public class User
	{
		public Guid UserId { get; set; }
		public string Email { get; set; }
		public string FullName { get; set; }
		public string Password { get; set; }
	}
}

