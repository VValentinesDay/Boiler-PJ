using Domain.DTO.UserDTO;
using Domain.Repositories.IUserRepository;
using Microsoft.EntityFrameworkCore;
using Model.Entities.Users;
using System.Text;
using XSystem.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Model.Repository.AutontificationRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public string AddUser(UserDTO userDTO)
        {
            if (_context.User.Any(x => x.Id == userDTO.Id) == true) 
            {
                throw new Exception("Пользователь уже существует");
            }
            

            var user = new UserEntity() { Id = userDTO.Id, Name = userDTO.Name, RoleID = (Role)userDTO.Role};
            _context.User.Add(user);

            // метод под пароль - то что вносит пользователь + соль


            user.Salt = new byte[16];
            new Random().NextBytes(user.Salt);
            var data = Encoding.UTF8.GetBytes(userDTO.Password).Concat(user.Salt).ToArray();
            user.Password = new SHA512Managed().ComputeHash(data);
            _context.SaveChanges();

            return user.Name;
        }

        // Рзница с авторизацией??
        public UserRoleDTO CheckUser(LoginDTO login)
        {

            //необходимо найти пользователя и сопоставить пароли

            var user = _context.User.FirstOrDefault(x => x.Name == login.Name);

            if (user == null) { throw new Exception("Такого пользователя не существует"); }


            var loginPassword = Encoding.UTF8.GetBytes(login.Password).Concat(user.Salt).ToArray();
            var hashPassword = new SHA512Managed().ComputeHash(loginPassword);

            if (user.Password.SequenceEqual(hashPassword))
            { return (UserRoleDTO)user.RoleID; }
            else
            { throw new Exception("Несовпадение паролей"); }
        }

        public string DeleteUser(string name)
        {
            if (_context.User.Any(x => x.Name == name) != true)
            {
                throw new Exception("Пользователь не существует");
            }

            _context.User.Where(x => x.Name == name).ExecuteDelete();
            _context.SaveChanges();

            return name;    
        }

        public List<string> GetAllUsers()
        {
            // неплохо было бы поставить проверку на роль

            var users = _context.User.Select(x => x.Name).ToList();
            return users;
        }
    }
}
