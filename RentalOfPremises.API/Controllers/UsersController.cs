using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalOfPremises.Application.DTOs.UserDto;
using RentalOfPremises.Application.Interfaces;

namespace RentalOfPremises.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        public readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)] // Успешная регистрация
        [ProducesResponseType(StatusCodes.Status409Conflict)] // Конфликт (email уже существует)
        public async Task<ActionResult> Register(UserRegisterRequest registerRequest)
        {
            var userId = await _userService.Register(registerRequest);
            
            if (userId == Guid.Empty)
            {
                return Conflict(new
                {
                    error = "EmailAlreadyExists",
                    message = "The provided email is already registered."
                });
            }
            else
            {
                return Ok(userId);
            }
        }

        //TODO: Метод Login выкидывает исключения при неправильном пароле или email.
        //Надо их либо обработать, либо придумать другой способ оповещения пользователя об ошибке
        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserLoginRequest loginRequest)
        {
            var jwtToken = await _userService.Login(loginRequest);

            HttpContext.Response.Cookies.Append("token", jwtToken);

            return Ok();
        }

        
    }
}
