using System;
using Identity.Core.Entities;

namespace Identity.Core.Domain.Repositories
{
	public interface IUserRepository
	{
		Task<User> CreateUser(User user);
        User? GetUserById(Guid userId);
        User? GetUserByEmail(string email);
		User? GetUserByEmailAndNotById(string email, Guid userId);
		List<User> GetUsers();
		void Delete(Guid userId);
		User UpdateUser(Guid userId, User user);
		string? Authenticate(string email, string password);
		Task Commit();
	}
}

