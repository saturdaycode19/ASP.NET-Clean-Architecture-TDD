using System;
using Identity.API.Controllers;
using Identity.Core.Domain.Params;
using Identity.Core.Domain.Repositories;
using Identity.Core.Entities;
using Identity.Test.FakeData.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Identity.Test.Controllers
{
	[TestClass]
	public class UserControllerTest
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

		[TestMethod, TestCategory("User_Controller")]
		public async Task CreateUserApi_Success()
		{
            // arrange
            var fakeUser = FakeDataUser.GetFakeUser();

            userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(value: null);
            userRepository.Setup(x => x.CreateUser(It.IsAny<User>())).Returns(Task.FromResult(fakeUser));

			// act

			var userController = new UserController(userRepository.Object);
			var result = await userController.CreateUserApi(paramCreateUser);

			var okResult = result as OkObjectResult;

			//assert
			Assert.AreEqual(okResult.StatusCode, 200);
        }

        [TestMethod, TestCategory("User_Controller")]
        public async Task CreateUserApi_Failed()
        {
            // arrange
            var fakeUser = FakeDataUser.GetFakeUser();

            userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(value: fakeUser);
            userRepository.Setup(x => x.CreateUser(It.IsAny<User>())).Returns(Task.FromResult(fakeUser));

            // act

            var userController = new UserController(userRepository.Object);
            var result = await userController.CreateUserApi(paramCreateUser);

            var badRequest = result as BadRequestObjectResult;

            //assert
            Assert.AreEqual(badRequest.StatusCode, 400);
        }
    }
}

