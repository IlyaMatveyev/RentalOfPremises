using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentalOfPremises.API.Extensions;
using RentalOfPremises.Application.DTOs;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.API.Controllers
{
    [ApiController]
    public class PremisesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPremiseService _premiseService;
        public PremisesController(
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
            //Достаём userId из клеймов с помощью Extension метода
            var userId = HttpContext.GetUserId();
            if(userId == Guid.Empty)
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

        [HttpGet("GetById/{premiseId:guid}")]
        [Authorize]
        public async Task<ActionResult<PremiseResponse>> GetById([FromRoute]Guid premiseId)
        {
            //Достаём userId из клеймов с помощью Extension метода
            var userId = HttpContext.GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new
                {
                    error = "Unauthorized",
                    message = "User ID is not valid or missing."
                });
            }

            var premises = await _premiseService.GetById(premiseId, userId);

            if (premises == null)
            {
                return BadRequest(new
                {
                    massage = "Premises not found or you are not Owner."
                });
            }
            return _mapper.Map<Premise, PremiseResponse>(premises);
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<ActionResult<List<PremiseResponse>>> GetAll()
        {
            //Достаём userId из клеймов с помощью Extension метода
            var userId = HttpContext.GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new
                {
                    error = "Unauthorized",
                    message = "User ID is not valid or missing."
                });
            }

            var premisesList = await _premiseService.GetAll(userId);


            if (premisesList.IsNullOrEmpty())
            {
                return BadRequest(new
                {
                    massage = "Premises not found."
                });
            }

            return Ok(premisesList.Adapt<List<PremiseResponse>>());
        }

        [HttpDelete("Delete/{premisesId:guid}")]
        [Authorize]
        public async Task<ActionResult> Delete([FromRoute] Guid premisesId)
        {
            //Достаём userId из клеймов с помощью Extension метода
            var userId = HttpContext.GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new
                {
                    error = "Unauthorized",
                    message = "User ID is not valid or missing."
                });
            }

            var countOfDeletedObj = await _premiseService.Delete(premisesId, userId);

            if(countOfDeletedObj == 0)
            {
                return BadRequest(new
                {
                    massage = "Premises not found or you are not Owner."
                });
            }
            return Ok($"Number of deleted records: {countOfDeletedObj}");
        }

        [HttpPut("Update/{premisesId:guid}")]
        [Authorize]
        public async Task<ActionResult<Guid>> Update([FromRoute] Guid premisesId, [FromBody] PremisesUpdateRequest premisesUpdateRequest)
        {
            //Достаём userId из клеймов с помощью Extension метода
            var userId = HttpContext.GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new
                {
                    error = "Unauthorized",
                    message = "User ID is not valid or missing."
                });
            }

            //маппинг
            var premises = _mapper.Map<PremisesUpdateRequest, Premise>(premisesUpdateRequest);

            var updatedPremisesId = await _premiseService.Update(premisesId, premises, userId);


            if (updatedPremisesId == Guid.Empty)
            {
                return NotFound("You don't have this premises");
            }

            return Ok(updatedPremisesId);
        }
    }
}
