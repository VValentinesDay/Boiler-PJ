namespace Model.Entities.Users
{
    public enum Role
    {
        //Admin,
        //Worker,
        //Client

        // примечательно, что в БД хранится номер,
        // а при аутентификации - имя 


        Admin = 0,
        Worker = 1,
        Client = 2
        
    }
}
