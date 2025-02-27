using Domain.DTO.UserDTO;

namespace Domain.Repositories.IUserRepository
{
    public interface IUserAutoantificationService
    {
        UserDTO Autoantification(LoginDTO login);
    }
}
