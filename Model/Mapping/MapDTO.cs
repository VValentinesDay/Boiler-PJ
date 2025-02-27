using Domain.DTO;
using Domain.DTO.UserDTO;
using Model.Entities;
using Model.Entities.Users;

namespace Model.Mapping
{



    public static class MappingExtensions
    {
        public static BoilerRoomDTO MapToDto(this BoilerRoomEntity? mappingObject)
        {

            if (mappingObject == null)
                throw new ArgumentNullException(nameof(mappingObject));
            BoilerRoomDTO dest = new()
            {
                //  public Guid Id { get; set; }
                //public string Name { get; set; }
                //public string? Description { get; set; }
                //public string? Adress { get; set; }

                Id = mappingObject.Id,
                Name = mappingObject.Name,
                Description = mappingObject.Description,
                Adress = mappingObject.Adress,
                CompanyName = mappingObject.CompanyName
                
            };

            return dest;

        }
        public static BoilerDeviceDTO MapToDto(this BoilerDevicesEntity? mappingObject)
        {


            if (mappingObject == null)
                throw new ArgumentNullException(nameof(mappingObject));
            BoilerDeviceDTO dest = new()
            {

                Id = mappingObject.Id,
                BoilerRoomName = mappingObject.BoilerRoomName,
                Name = mappingObject.Name,
                Type = mappingObject.Type,
                NominalValue = mappingObject.NominalValue,
                Installed = mappingObject.Installed,
                Notes = mappingObject.Notes,
                Instruction = mappingObject.Instruction
            };
            return dest;
        }
        public static CompanyDTO MapToDTO(this CompanyEntity? mappingObject)
        {
            if (mappingObject == null) throw new ArgumentNullException(nameof(mappingObject));
            CompanyDTO dest = new() { 
                ContactPerson = mappingObject.ContactPerson, 
                ContactPersonNumber = mappingObject.ContactPersonNumber, 
                Id = mappingObject.Id,
                Name = mappingObject.Name};
            return dest;   
        }

        public static UserDTO MapToDTO(this UserEntity? mappingObject) 
        {
            if(mappingObject == null) throw     new ArgumentNullException(nameof(mappingObject));
            UserDTO dest = new() 
            { Name = mappingObject.Name, 
              Id = mappingObject.Id,

              // чтобы не создавать лишних зависимостей и нового метода для маппинга
              Role = (UserRoleDTO)mappingObject.RoleID
            };
            
            return dest;
        }
        
    }

}


public record EntityForDTO
{
    BoilerRoomEntity? entity { get; set; }
    BoilerDevicesEntity? device { get; set; }
    CompanyEntity? company { get; set; }
}


