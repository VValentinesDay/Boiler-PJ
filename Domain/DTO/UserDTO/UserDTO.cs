namespace Domain.DTO.UserDTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public UserRoleDTO Role { get; set; }
        //public Role Role { get; set; }
    }
}
