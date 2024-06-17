using System;
using Identity.Application.UseCases.Users;
using Identity.Core.Domain.Params;
using Identity.Core.Domain.Repositories;
using Identity.Core.Entities;
using Identity.Test.FakeData.Users;
using Moq;

namespace Identity.Test.Application.Usecases.Users
{
	[TestClass]
	public class CreateUserTest
	{

		Mock<IUserRepository> userRepository;
		ParamCreateUser paramCreateUser;

		[TestInitialize]
		public void Setup()
		{
			userRepository = new Mock<IUserRepository>();

			paramCreateUser = new ParamCreateUser();
			paramCreateUser.FullName = "Administrator";
			paramCreateUser.Email = "admin@admin.com";
			paramCreateUser.Password = "12345";
		}

		[TestMethod, TestCategory("CreateUser_Usecase")]
		public async Task CreateUser_Success()
		{
			// arrange
			var fakeUser = FakeDataUser.GetFakeUser();

			userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(value: null);
			userRepository.Setup(x => x.CreateUser(It.IsAny<User>())).Returns(Task.FromResult(fakeUser));

			// act
			var createUser = new CreateUser(userRepository.Object);
			var result = await createUser.Handle(paramCreateUser);

			// assert
			Assert.AreEqual(result.Message, null);
		}

        [TestMethod, TestCategory("CreateUser_Usecase")]
        public async Task CreateUser_Failed()
        {
            // arrange
            var fakeUser = FakeDataUser.GetFakeUser();

            userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(value: fakeUser);
            userRepository.Setup(x => x.CreateUser(It.IsAny<User>())).Returns(Task.FromResult(fakeUser));

            // act
            var createUser = new CreateUser(userRepository.Object);
            var result = await createUser.Handle(paramCreateUser);

            // assert
            Assert.AreNotEqual(result.Message, null);
        }
    }
}

