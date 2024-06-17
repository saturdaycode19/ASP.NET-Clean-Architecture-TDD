using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Sockets;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Identity.Core.Domain.Repositories;
using Identity.Core.Entities;
using Identity.Infrastructure.Data;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure.RepositoryImpl
{
	public class UserRepositoryImpl : IUserRepository
	{
        private readonly Context db;
		public UserRepositoryImpl(Context db)
		{
            this.db = db;
		}

        public string? Authenticate(string email, string password)
        {
            var user = db.Users.SingleOrDefault(x => x.Email == email && x.Password == Hash(password));
            if (user == null) return null;

            var token = BuildToken(user);

            return token;
        }

        public async Task Commit()
        {
            await db.SaveChangesAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            user.UserId = Guid.NewGuid();
            user.Password = Hash(user.Password);
            await db.Users.AddAsync(user);
            return user;
        }

        public void Delete(Guid userId)
        {
            var userdb = db.Users.Single(x => x.UserId == userId);
            db.Users.Remove(userdb);
            return;
        }

        public User? GetUserByEmail(string email)
        {
            return db.Users.SingleOrDefault(x => x.Email == email);
        }

        public User? GetUserByEmailAndNotById(string email, Guid userId)
        {
            return db.Users.SingleOrDefault(x => x.Email == email && x.UserId != userId);
        }

        public User? GetUserById(Guid userId)
        {
            return db.Users.SingleOrDefault(x => x.UserId == userId);
        }

        public List<User> GetUsers()
        {
            return db.Users.ToList();
        }

        public User UpdateUser(Guid userId, User user)
        {
            var userdb = db.Users.Single(x => x.UserId == userId);
            user.Password = Hash(user.Password);
            db.Entry(userdb).CurrentValues.SetValues(user);
            return user;
        }

        private string Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        private string BuildToken(User user)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("project-x—very-very-very-very-secret-key");

            var claims = new ClaimsIdentity(new Claim[]
                {
                new Claim(JwtRegisteredClaimNames.GivenName, user.FullName),
                new Claim("email", user.Email != null ? user.Email:string.Empty),
                });

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.Now.AddHours(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

