using System;
using Identity.Core.Entities;

namespace Identity.Test.FakeData.Users
{
	public static class FakeDataUser
	{
		public static User GetFakeUser()
		{
			return new User()
			{
				UserId = Guid.Parse("2a5ec03f-5d71-4b89-9ba2-06b938be9403"),
				Email = "admin@admin.com",
				FullName = "Administrator",
				Password = "8CB2237D0679CA88DB6464EAC60DA96345513964"
			};
		} 
	}
}

