using Domain.DTO;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Model.Entities;
using Model.Mapping;

namespace Model.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        readonly private Context _context;
        readonly private IMemoryCache _cache;
        public async Task<Guid> Create(BoilerDeviceDTO devicesDTO)
        {


            if (_context.BoilerDevices.Any(x => x.Id == devicesDTO.Id) == true)
            { throw new Exception("Объект с таким именем уже существует"); }
            var device = new BoilerDevicesEntity()
            {
                Id = devicesDTO.Id,
                BoilerRoomName = devicesDTO.BoilerRoomName,
                Type = devicesDTO.Type,
                Installed = devicesDTO.Installed,
                Instruction = devicesDTO.Instruction,
                Name = devicesDTO.Name,
                NominalValue = devicesDTO.NominalValue,

            };

            await _context.BoilerDevices.AddAsync(device);
            await _context.SaveChangesAsync();
            _cache.Remove("devices");
            return device.Id;

        }

        public async Task<string> Delete(Guid guid)
        {
            if (_context.BoilerDevices.Any(x => x.Id == guid) == false)
            { throw new Exception("Такого объекта не существует"); }
            await _context.BoilerDevices.Where(x => x.Id == guid).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return $"Объект с ID:\"{guid}\" - удалён";
        }

        public async Task<BoilerDeviceDTO> Get(Guid guid)
        {
            if (_context.BoilerDevices.Any(y => y.Id == guid) == false)
            { throw new Exception("Объект с таким именем уже существует"); }
            var device = await _context.BoilerDevices.FirstOrDefaultAsync(x => x.Id == guid);
            return device.MapToDto();
        }

        public async Task<List<BoilerDeviceDTO>> GetAll()
        {
            if (_context.BoilerDevices.ToList().Count == 0)
            { throw new Exception("Элементы отсутсвуют"); }


            var listDTO = await _context.BoilerDevices.Select(x => x.MapToDto()).ToListAsync();
            return listDTO;
        }

        public async Task<string> Update(string name, string note, string instruction, int nomanal, DateTime updated)
        {
            if (_context.BoilerDevices.Any(x => x.Name == name) == false)
            { throw new Exception("Такого объекта не существует"); }
            await _context.BoilerDevices.Where(x => x.Name == name).
                ExecuteUpdateAsync(x => x.
                SetProperty(x => x.Notes, note).
                SetProperty(x => x.Instruction, instruction).
                SetProperty(x => x.NominalValue, nomanal).
                SetProperty(x => x.Updated, updated));

            await _context.SaveChangesAsync();
            return $"Объект с ID:\"{_context.BoilerDevices.FirstOrDefaultAsync(x => x.Name == name).Id}\" - обновлён.";

        }



    }



}
