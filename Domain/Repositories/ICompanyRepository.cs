using Domain.DTO;

namespace Domain.Repositories
{
    public interface ICompanyRepository
    {
        public Task<List<CompanyDTO>> GetAll();
        public Task<CompanyDTO> Get(string companyname);
        public Task<string> Create(CompanyDTO company);

        public Task<string> Update(string name, string contactPerson, int contactNumber);
        //public Task<Guid> Delete(Guid id);
        public  Task<string> Delete(string name);

        public Task<CompanyDTO> EditGetInfo(string name);

        public Task<string> Edit(CompanyDTO company);


    }
}
