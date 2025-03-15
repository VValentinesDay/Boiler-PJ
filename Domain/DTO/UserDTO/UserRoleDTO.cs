namespace Domain.DTO.UserDTO
{
    public enum UserRoleDTO
    {
        //Admin,
        //Worker,
        //Client
     
        // примечательно, что в БД хранится номер,
        // а при аутентификации - имя 
        Admin = 0,
        Worker = 1,
        Client = 2,
    }
}
