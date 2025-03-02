using Domain.DTO.UserDTO;
using Domain.Repositories.IUserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities.Users;

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

        [HttpPost("Create")]
        public IActionResult CreateUser(UserDTO user) 
        {
            _userRepository.AddUser(user);
            return Ok(user.Name);    
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(string name) 
        {
            _userRepository.DeleteUser(name);
            return Ok(name);
        }

        [HttpPost]
        public ActionResult<Role> CheckUser(LoginDTO login) 
        {
            return Ok(_userRepository.CheckUser(login));
        }
    }
}
