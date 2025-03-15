using Domain.DTO.UserDTO;
using Domain.Repositories.IUserRepository;
using kotl.RSAkeys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace kotl.Controllers
{

    [ApiController]
    [Route("[controller]")] //ВМЕСТО controller БУДЕТ ПОДСТАВЛЕНО НАИМЕНОВАНИЕ КОНТРОЛЛЕРА
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserAutoantificationService _userAutoantification;
        //private readonly Context _context;

        public LoginController(IConfiguration configuration, IUserAutoantificationService userAutoantification)
        {
            _userAutoantification = userAutoantification;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] LoginDTO loginDTO)
        // атрибут указывает на то что параметр будет получен из тела запроса
        {
            // попытка аутентификации
            var user = _userAutoantification.Autoantification(loginDTO);
            if (user != null)
            {
                var token = GenToken(user);
                /*
                 Токен генерируется под пользователя, а не под логин/пароль
                 */
                return Ok(token);
            }
            return NotFound("Пользователь не найден");

        }



        // Встроена какая-то защита: при модифиакторе public - сервер
        // выдаёт ошибку 500, а терминал говорит о неоднозначности
        // метода GetToken. Необходим private
        private string GenToken(UserDTO user)
        {
            /*
            // ключ извлекается из поля токена JWT из Appsettings. Он нужен для подписи токенов
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            // объект принимает ключ и алгоритм для шифрования - используется для подписи токена
            // зарегистрированные полномочия
            var tokenMeneger = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            */

            var key = new RsaSecurityKey(keyGetterRSA.GetPrivateKey());
            var credentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);

            // сlaims - данные, которые содержат токен: имя пользователя и его роль
            // здесь задаётся соответсвие модели ДТО и типы утверждений/Claims
            var claims = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier, user.Name),
                 new Claim(ClaimTypes.Role, user.Role.ToString())
                 
             };

            // создание объекта токена 
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],// указывает на создаетля токена
                _configuration["Jwt:Audience"],// кому предназначен токен
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            // получение строки токена
            return new JwtSecurityTokenHandler().WriteToken(token);
        }








    }
}
