using Microsoft.EntityFrameworkCore;
using Model;
using Model.Entities.Users;

namespace kotlTest.Common
{
   
    public class ContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid NoteIdForDelete = Guid.NewGuid();
        public static Guid NoteIdForUpdate = Guid.NewGuid();

        public static Context Create()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new Context(options);
            context.Database.EnsureCreated(); // убедиться что бд создана
            context.
                // тестовые данные
                // соль и пароль?? - взять с бд
                // всё таки там хранится зашироаныые данные
                User.AddRange(
                new Model.Entities.Users.UserEntity
                {
                    Id = 1,
                    Name = "Sasha",
                    RoleID = Role.Admin,
                        
                },
                new Model.Entities.Users.UserEntity
                {
                    Id = 2,
                    Name = "Sasha",
                    RoleID= Role.Worker,
                }
            );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(Context context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}

