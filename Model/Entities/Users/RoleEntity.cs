namespace Model.Entities.Users
{
    public class RoleEntity
    {
        public string Name { get; set; }    
        public Role Role { get; set; }
        public virtual List<UserEntity>? Users { get; set; } 
    }
}
