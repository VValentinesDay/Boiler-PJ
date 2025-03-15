using Domain.Repositories.IUserRepository;
using kotl.Controllers;
using Model.Entities.Users;
using Moq;
using XAct.Users;

namespace kotlTest
{
    public class UserControllerTest 
    {
        [Fact]
        public void IndexReturnsAViewResultWithAListOfUsers()
        {
            //private readonly IUserRepository _userRepository;

            // Arrange
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.GetAllUsers()).Returns(GetTestUsers());
            var controller = new UserController(mock.Object);

            // Act
            var result = controller.GetAllUsers();

            // Assert
            var viewResult = Assert.IsType<Microsoft.AspNetCore.Mvc.ActionResult<List<string>>>(result);
            var model = Assert.IsAssignableFrom<Microsoft.AspNetCore.Mvc.ActionResult<List<string>>>(viewResult);
            Assert.Equal(GetTestUsers().Count, 4);
        }
        private List<string> GetTestUsers()
        {
            var users = new List<UserEntity>
            {
                new UserEntity { Id=1, Name="Tom"},
                new UserEntity { Id=2, Name="Alice"},
                new UserEntity { Id=3, Name="Sam"},
                new UserEntity { Id=4, Name="Kate"}
            };

            var names = users.Select(x => x.Name).ToList();
            return names;
        }
    }
}