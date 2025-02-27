namespace Model.Entities.Users
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public Role RoleID { get; set; }
        public virtual RoleEntity? Roles { get; set; }
    }
}