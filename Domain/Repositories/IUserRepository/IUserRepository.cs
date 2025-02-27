using Domain.DTO.UserDTO;

namespace Domain.Repositories.IUserRepository
{
    public interface IUserRepository
    {
        List<string> GetAllUsers();
        string AddUser(UserDTO userDTO);
        string DeleteUser(string name);
        UserRoleDTO CheckUser(LoginDTO login);
    }
}
