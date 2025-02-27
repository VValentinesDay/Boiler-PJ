using Domain.DTO;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace kotl.Controllers
{
    [ApiController]
    [Route("[controller]")] //ВМЕСТО controller БУДЕТ ПОДСТАВЛЕНО НАИМЕНОВАНИЕ КОНТРОЛЛЕРА
    public class BoilerRoomController: ControllerBase
    {
        private readonly IBoilerRepository _boilerRepository;
        private readonly Context _context;

        public BoilerRoomController(Context context, IBoilerRepository boilerRepository) 
        {
            _boilerRepository = boilerRepository;
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<BoilerRoomDTO>>> GetAllBoilers() 
        {
            return Ok(await _boilerRepository.GetAllBoiler()); 
        }


        //переделать под запрос frombody
       //[HttpGet]
       // public async Task<ActionResult<BoilerDeviceDTO>> GetBoilerRoom([FromQuery] string name)
       // {
       //     return Ok(await _boilerRepository.GetBoilerName(name));
       // }

        [HttpPost]
        public async Task<IActionResult> CreateBoilerRoom(BoilerRoomDTO boiler) 
        {
            //try 
            //{
            //    await _boilerRepository.CreateBoiler(boiler);

            //    return Ok("котельная добавлена");
            //}
            //catch { throw new Exception("Ошибка добавления"); }


            await _boilerRepository.CreateBoiler(boiler);

            return Ok("котельная добавлена");


        }


        [HttpPatch]
        public async Task<IActionResult> UpdateBoilerRoom(string name, string discription, string adress)

        {
            await _boilerRepository.UpdateBoiler(name, discription, adress);
            return Ok($"Котельная \"{name}\" - обновлена");
        }

    
        [HttpDelete]
        public async Task<IActionResult> DeleteBoilerRoom(string name) 
        { 
            await _boilerRepository.DeleteBoiler(name);
            return Ok($"Котельная \"{name}\" - удалена");
        }

    }
}
