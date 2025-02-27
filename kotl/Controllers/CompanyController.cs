using Domain.DTO;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace kotl.Controllers
{
    [ApiController]
    [Route("[controller]")] //ВМЕСТО controller БУДЕТ ПОДСТАВЛЕНО НАИМЕНОВАНИЕ КОНТРОЛЛЕРА
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly Context _context;

        public CompanyController(Context context, ICompanyRepository boilerRepository)
        {
            _companyRepository = boilerRepository;
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<CompanyDTO>>> GetAllCompany()
        {
            return Ok(await _companyRepository.GetAll());
        }

        //[HttpGet]
        //public async Task<IActionResult> GetCompany([FromQuery] string name) 
        //{
        //    var company = await _companyRepository.Get(name);
        //    return Ok(company);
        //}

        [HttpPost]
        public async Task<IActionResult> CompanyRoom(CompanyDTO company)
        {
            //try 
            //{
            //    await _boilerRepository.CreateBoiler(boiler);

            //    return Ok("котельная добавлена");
            //}
            //catch { throw new Exception("Ошибка добавления"); }


            await _companyRepository.Create(company);

            return Ok("котельная добавлена");


        }

        [HttpPatch] // put меняет объект целиком, putch только указанные поля
        public async Task<IActionResult> UpdateCompany(string name, string contactPerson, int contactNumber)
        {
             await _companyRepository.Update(name, contactPerson, contactNumber);
            return Ok($"Комапния \"{name}\" - обновлена");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCompany(string name)
        {
            await _companyRepository.Delete(name);
            return Ok($"Комапния \"{name}\" - удалена");
        }


        //[HttpPatch]
        //public async Task<CompanyDTO> EditGet(string name)
        //{
        //    var companyForEdit = await _companyRepository.EditGetInfo(name);
        //    await _companyRepository.Edit(companyForEdit);
        //    return companyForEdit;
        //}

       

        //[HttpPatch]
        //public async Task<CompanyDTO> Edit(string name)
        //{
        //    await _companyRepository.Edit(EditGet);
        //    return companyForEdit;
        //}
    }
}
