using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalOfPremises.Application.DTOs;
using RentalOfPremises.Application.Interfaces;

namespace RentalOfPremises.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public UserController(IUserService userService)
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
                //return Results.BadRequest();
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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> getall()
        {
            return Ok(await _userService.getAll());
        }
    }
}
