using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Entities.Users;

namespace Model
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<BoilerRoomEntity> BoilerRoom { get; set; }
        public DbSet<BoilerDevicesEntity> BoilerDevices { get; set; }
        public DbSet<CompanyEntity> Company { get; set; }


        public DbSet<UserEntity> User { get; set; }
        public DbSet<RoleEntity> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoilerRoomEntity>(entity =>
            {
                entity.HasKey(x => x.Id).HasName("ID_BoilerRoom");

                // еще один ключ 
                entity.HasKey(x => x.Name);

                // наименование таблицы
                entity.ToTable("BoilerRoom");
                // Property(pg => pg.Name) - задаёт колоке имя из класса.
                // Следющий метод,задаёт другое имя, при необходимости

                entity.Property(x => x.Name).
                HasColumnName("name").
                HasMaxLength(255);

                entity.Property(d => d.Description).
                HasColumnName("description").
                HasMaxLength(500);

                entity.Property(a => a.Adress).
                HasColumnName("adress").
                HasMaxLength(250);

                //связи
                // связи образуются через виртуальные свойства. Те сущности, к которым объект относится
                // как ко многим задаются через List<>, как к одному - через одну сущность-поле
                entity.HasOne(x => x.Company).
                WithMany(x => x.BoilerRoomEntities).
                HasForeignKey(x => x.CompanyName);

                // Для DEVICE - обратная связь
                //entity.HasMany(x=>x.BoilerDevices).WithOne(x=>x.BoilerRoom).HasForeignKey(x=>x.)

            }
                );

            modelBuilder.Entity<CompanyEntity>(entity =>
            {
                entity.HasKey(pg => pg.Id);

                //Ключ по названию компании
                entity.HasKey(entity => entity.Name);

                entity.HasIndex(x => x.Name).IsUnique();
                entity.Property(pg => pg.Name).
                HasColumnName("name").
                HasMaxLength(255);
                entity.Property(num => num.ContactPersonNumber);
                entity.Property(num => num.ContactPerson);

                // связи
                //entity.HasMany(x => x.BoilerRoomEntities).
                //WithOne(x => x.Company).
                //HasForeignKey(x => x.Company); // через что ссылается сущность 


            }

        );

            modelBuilder.Entity<BoilerDevicesEntity>(entity =>
            {
                entity.HasKey(pg => pg.Id);
                entity.Property(entity => entity.Name);
                entity.Property(entity => entity.Installed);
                entity.Property(entity => entity.Type);
                entity.Property(entity => entity.NominalValue);
                entity.Property(entity => entity.Instruction);

                entity.HasOne(p => p.BoilerRoom).
                WithMany(p => p.BoilerDevices).
                HasForeignKey(p => p.BoilerRoomName);
            }
            );


            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(x => x.Salt);
                //entity.Property(x => x.Roles);
                entity.Property(x => x.RoleID);


                entity.HasOne(x => x.Roles)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleID);

                entity.Property(x => x.RoleID)
                .HasConversion<int>();
            }
            );
            modelBuilder.Entity<RoleEntity>(entity =>
            {

                entity.Property(x => x.Role);
                entity.Property(x => x.Name);
                entity.HasKey(x => x.Role);

                entity.Property(x => x.Role)
                .HasConversion<int>();

                entity.HasData(Enum.GetValues(typeof(Role))
                    .Cast<Role>()
                    .Select(x => new RoleEntity
                    { Role = x, Name = x.ToString() }));
            }
            );
        }
    }
}
