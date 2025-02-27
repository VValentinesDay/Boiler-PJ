using Domain.DTO.UserDTO;
using Domain.Repositories.IUserRepository;
using Model.Mapping;
using System.Text;
using XSystem.Security.Cryptography;

namespace Model.Repository.AutontificationRepository
{
    public class UserAutoantificationService : IUserAutoantificationService
    {

        private readonly Context _context;

        public UserAutoantificationService(Context context)
        {
            _context = context;
        }

        public UserDTO Autoantification(LoginDTO login)
        {
            if (login == null) { throw new ArgumentNullException("Логин не введён"); }

            try
            {
                var userEntity = _context.User.FirstOrDefault(x => x.Name == login.Name);
                // может понадобиться метод ToArray
                var passwordToCompare = Encoding.UTF8.GetBytes(login.Password).Concat(userEntity.Salt);
                
                var hash = new SHA512Managed().ComputeHash(passwordToCompare.ToArray());

                if (hash.SequenceEqual(userEntity.Password))
                {
                    return userEntity.MapToDTO();
                }
                else 
                {
                    throw new Exception("Несоответсвие пароля");
                }
            }
            catch 
            
            { throw new Exception("Пользователя не существует"); 
            }
        }
    }
}
