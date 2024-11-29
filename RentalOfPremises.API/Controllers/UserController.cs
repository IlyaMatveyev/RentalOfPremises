using Microsoft.AspNetCore.Mvc;

namespace RentalOfPremises.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class USerController : ControllerBase
    {
        [HttpPost("reg")]
        public async Task<IResult> Registr()
        {
            return Results.Ok();
        }

        [HttpPost("login")]
        public async Task<IResult> Login()
        {
            return Results.Ok();
        }
    }
}
