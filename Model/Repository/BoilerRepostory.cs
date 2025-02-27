using Domain.DTO;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Model.Entities;
using Model.Mapping;

namespace Model.Repository
{
    public class BoilerRepostory : IBoilerRepository
    {
        private readonly Context _context;
        private readonly IMemoryCache _cache;

        public BoilerRepostory(Context context, IMemoryCache memoryCache)
        {
            _context = context;
            _cache = memoryCache;
        }

        public async Task<List<BoilerRoomDTO>> GetAllBoiler()
        {
            //using var context = new HRContext();
            //var list = context.Employees.Select(e => new { e.FirstName, e.JoinedDate }).ToList();

            //Нужно перевести entity в DTO - несовпадение элементов списка

            //Могут возникнуть ошибки 
            if(_context.BoilerRoom.ToList().Count == 0) { throw new Exception("Список объектов пуст"); }
            if (_cache.TryGetValue("AllBoilers", out List<BoilerRoomDTO> cacheList)) return cacheList;

            var list = await _context.BoilerRoom.Select(x => x.MapToDto()).ToListAsync();
            _cache.Set("AllBoilers", list, TimeSpan.FromMinutes(30));


            return list;

        }

        public async Task<BoilerRoomDTO> GetBoilerId(Guid id)
        {
            // в кэшировании не особо много смысла
            if (_context.BoilerRoom.Select(x => x.Id == id).ToList() == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                var boilerRoom = await _context.BoilerRoom.FirstOrDefaultAsync(x => x.Id == id);

                return boilerRoom.MapToDto();
            }
        }

        public async Task<Guid> CreateBoiler(BoilerRoomDTO boiler)
        {
            if (_context.BoilerRoom.Any(x => x.Name == boiler.Name) == true)
            {
                throw new Exception("Котельная с таким названием уже существует");
            }
            else
            {
                // теперь необходимо из DTO получить entity
                var boilerentity = new BoilerRoomEntity()
                {
                    Id = boiler.Id,
                    Name = boiler.Name,
                    Description = boiler.Description,
                    Adress = boiler.Adress,
                    CompanyName = boiler.CompanyName,
                        
                };
                _cache.Remove("AllBoilers");
                await _context.BoilerRoom.AddAsync(boilerentity);
                await _context.SaveChangesAsync();

                return boilerentity.Id;


            }
        }

        public async Task<string> DeleteBoiler(string name)
        {
            if (_context.BoilerRoom.Any(x => x.Name == name) == false)
            {
                throw new Exception("Объекта с таким именем не существует");
            }
            var boiler = await _context.BoilerRoom.FirstOrDefaultAsync(x => x.Name == name);

           await _context.BoilerRoom.Where(x => x.Name == name).ExecuteDeleteAsync();
            _cache.Remove("AllBoilers");

            return boiler.Name;
        }


        public async Task<BoilerRoomDTO> GetBoilerName(string name)
        {
            if (_context.BoilerRoom.Any(y => y.Name == name) == false) 
            { throw new Exception("Объекта с таким именем не существует"); }
            var res = await _context.BoilerRoom.FirstOrDefaultAsync(x => x.Name == name);
            return res.MapToDto();
        }

        public async Task<Guid> UpdateBoiler(string name, string discription, string adress)
        {
            if (_context.BoilerRoom.Any(_x => _x.Name == name) == false)
            { throw new Exception("Такой объект уже существует"); }
            await _context.BoilerRoom.Where(x => x.Name == name).
                ExecuteUpdateAsync(x => x.
                SetProperty(x => x.Description, discription).
                SetProperty(x => x.Adress, adress));
            await _context.SaveChangesAsync();
            var id = await _context.BoilerRoom.FirstOrDefaultAsync(x => x.Name == name);
            return id.Id;
        }
    }
}
