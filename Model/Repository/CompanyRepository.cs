using Domain.DTO;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Model.Entities;
using Model.Mapping;

namespace Model.Repository
{
    delegate Task<CompanyDTO> GetDTOfoEdit(string name);

    public class CompanyRepository : ICompanyRepository
    {

        public CompanyRepository(Context context, IMemoryCache cache ) 
        { 
            _cache = cache;
            _context = context;
        }

        private readonly Context _context;
        private readonly IMemoryCache _cache;


        async public Task<string> Create(CompanyDTO company)
        {
            //if (_context.Company.Any(x => x.Name == company.Name) == true)
            //{
            //    throw new Exception("Такой объект уже существует");
            //}

            var newCompany = new CompanyEntity()
            {
                Id = company.Id,
                Name = company.Name,
                ContactPerson = company.ContactPerson,
                ContactPersonNumber = company.ContactPersonNumber
            };

            await _context.Company.AddAsync(newCompany);
            await _context.SaveChangesAsync();

            return newCompany.Name;
        }

        public async Task<string> Delete(string name)
        {
            if (_context.Company.Any(x => x.Name == name) == false)
            { throw new Exception("Такого объекта не существует"); }

            await _context.Company.Where(x => x.Name == name).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();

            return name;
        }

        async public Task<CompanyDTO> Get(string companyname)
        {
            if(_context.Company.Any(x=>x.Name==companyname) == false) { throw new Exception("Такого объекта не существует"); }
            var companyDTO =await  _context.Company.FirstOrDefaultAsync(x => x.Name == companyname);
            return companyDTO.MapToDTO();
        }

        async public Task<List<CompanyDTO>> GetAll()
        {
            if (_cache.TryGetValue("company", out List<CompanyDTO> dtoCache) == true) { return dtoCache; }
            var listDTO = await _context.Company.Select(x => x.MapToDTO()).ToListAsync();
            _cache.Set("company", listDTO, TimeSpan.FromMinutes(30));
            return listDTO;
        }

        async public Task<string> Update(string name, string contactPerson, int contactNumber)
        {
            if (_context.Company.Any(x => x.Name == name) == false) 
            { throw new Exception("Такого объекта не существует"); }

            await _context.Company.Where(x => x.Name == name).
                ExecuteUpdateAsync(x => x.
                SetProperty(x => x.ContactPersonNumber, contactNumber).
                SetProperty(x => x.ContactPerson, contactPerson));
            await _context.SaveChangesAsync();
            _cache.Remove("company");

            return ($"Данные \"{name}\" обновлены:  {contactPerson} {contactNumber}");
        }



        //GetDTOfoEdit GetDTO;; 
            //= EditGetInfo;

            


        async public Task<CompanyDTO> EditGetInfo(string name)
        {
            if (_context.Company.Any(x => x.Name == name) == false)
            { throw new Exception("Такого объекта не существует"); }
            var companyForEdit = await _context.Company.FirstOrDefaultAsync(x => x.Name == name);
            return companyForEdit.MapToDTO();
        }

        async public Task<string> Edit(CompanyDTO company)
        {
             await _context.Company.Where(x => x.Name == company.Name).
            ExecuteUpdateAsync(x => x.
            SetProperty(x => x.ContactPersonNumber, company.ContactPersonNumber).
            SetProperty(x => x.ContactPerson, company.ContactPerson));
            await _context.SaveChangesAsync();
            _cache.Remove("company");

            return ($"Данные \"{company.Name}\" обновлены:  {company.ContactPerson} {company.ContactPersonNumber}");
        }


    }
}
