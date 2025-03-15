using Domain.DTO.UserDTO;
using Domain.Repositories.IUserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities.Users;
using System.Security.Claims;

namespace kotl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {


        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult<List<string>> GetAllUsers() 
        {
             var users = _userRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser(UserDTO user) 
        {
            _userRepository.AddUser(user);
            return Ok(user.Name);    
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, Worker")]
        public IActionResult DeleteUser(string name) 
        {

            var currentUser = GetCurrentUser();
            if ( currentUser == null) 
            {
                throw new NullReferenceException();
            }
            if (currentUser.Role.ToString() != "Admin") 
            {
                throw new Exception("Вы не являетесь администратором");
            }

            _userRepository.DeleteUser(name);
            return Ok(name);
        }

        [HttpPost("CheckUser")]
        public ActionResult<Role> CheckUser(LoginDTO login) 
        {
            return Ok(_userRepository.CheckUser(login));
        }


        private UserDTO GetCurrentUser() 
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null) 
            {
                var userClaims = identity.Claims;
                return new UserDTO
                {
                    Name = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value,
                    Role = (UserRoleDTO)Enum.Parse(typeof(UserRoleDTO),
                            userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value)
                };
            }
            return null;
        }
    }
}
