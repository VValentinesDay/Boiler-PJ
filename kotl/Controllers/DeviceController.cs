using Domain.DTO;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace kotl.Controllers
{
    [ApiController]
    [Route("[controller]")] //ВМЕСТО controller БУДЕТ ПОДСТАВЛЕНО НАИМЕНОВАНИЕ КОНТРОЛЛЕРА
    public class DevicrController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly Context _context;

        public DevicrController(Context context, IDeviceRepository Repository)
        {
            _deviceRepository = Repository;
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<CompanyDTO>>> GetAllDevice()
        {
            return Ok(await _deviceRepository.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> CreateDevice(BoilerDeviceDTO deviceDTO)
        {
            //try 
            //{
            //    await _boilerRepository.CreateBoiler(boiler);

            //    return Ok("котельная добавлена");
            //}
            //catch { throw new Exception("Ошибка добавления"); }


            await _deviceRepository.Create(deviceDTO);

            return Ok($"Оборудование для котельной \"{deviceDTO.BoilerRoomName}\" добавлено");
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateDevice(string name, string note, string instruction, int nomanal, DateTime updated)
        {
            return Ok(await _deviceRepository.Update(name, note, instruction, nomanal, updated));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDevice(Guid id)
        {
            return Ok(await _deviceRepository.Delete(id));
        }


    }
}
