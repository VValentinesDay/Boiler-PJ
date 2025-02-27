using Domain.DTO;

namespace Domain.Repositories
{
    public interface IDeviceRepository
    {
        Task<List<BoilerDeviceDTO>> GetAll();


        // get можно, даже нужно осуществить через имя компании
        // (несколько единиц на выходе), по типу и компании,
        // по типу и котельной, по имени,
        // причём имя может быть указано не полностью --> реали
        // ориентироваться по ID сложновато, да и не нужно.
        Task<BoilerDeviceDTO> Get(Guid guid);


        Task<Guid> Create(BoilerDeviceDTO devicesDTO);
        //Task<string> Update(BoilerDevicesDTO devicesDTO);// имело бы смысл при поверке или ремонте,
        //// в целом же, оборудование не меняется по частям - ставят полностью новое, в случае чего можно 
        //// дать возможность редактировать поле notes, instructions, nomainalvalue. installedDate,  updated 

        Task<string> Update(string name, string note, string instruction, int nomanal, DateTime updated);
        Task<string> Delete(Guid guid);
    }
}
