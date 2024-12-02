using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalOfPremises.Application.DTOs;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.API.Controllers
{
    [ApiController]
    public class PremiseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPremiseService _premiseService;
        public PremiseController(
            IPremiseService premiseService, 
            IMapper mapper)
        {
            _mapper = mapper;
            _premiseService = premiseService;
        }

        [HttpPost]
        [Authorize]
        [Route("/Add")]
        public async Task<ActionResult<Guid>> Add(PremiseCreateRequest premiseCreateRequest)
        {
            //Достаём userId из клеймов JWT токена
            var userIdClaim = HttpContext.User.FindFirst("userId");
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized(new
                {
                    error = "Unauthorized",
                    message = "User ID is not valid or missing."
                });
            }

            //Маппинг
            var premise = _mapper.Map<PremiseCreateRequest, Premise>(premiseCreateRequest);
            
            return Ok(await _premiseService.Add(premise, userId));
        }
    }
}
