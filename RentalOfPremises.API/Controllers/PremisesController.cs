using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentalOfPremises.API.Extensions;
using RentalOfPremises.Application.DTOs.PremisesDto;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PremisesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPremisesService _premiseService;
        public PremisesController(
            IPremisesService premiseService, 
            IMapper mapper,
            IImageStorage imageStorage)
        {
            _mapper = mapper;
            _premiseService = premiseService;
        }

        [HttpPost("Add")]
        [Authorize]
        public async Task<ActionResult<Guid>> Add([FromForm]PremiseCreateRequest premiseCreateRequest)
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
            var premises = _mapper.Map<PremiseCreateRequest, Premises>(premiseCreateRequest);

            return Ok(await _premiseService.Add(premises, premiseCreateRequest.MainPhoto));
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

            var premises = await _premiseService.GetById(premiseId);

            if (premises == null)
            {
                return BadRequest(new
                {
                    massage = "Premises not found or you are not Owner."
                });
            }
            return _mapper.Map<Premises, PremiseResponse>(premises);
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

            var premisesList = await _premiseService.GetAll();


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

            var countOfDeletedObj = await _premiseService.Delete(premisesId);

            if(countOfDeletedObj == 0)
            {
                return BadRequest(new
                {
                    massage = "Premises not found or you are not Owner."
                });
            }
            return Ok($"Number of deleted object(s): {countOfDeletedObj}");
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
            var premises = _mapper.Map<PremisesUpdateRequest, Premises>(premisesUpdateRequest);

            var updatedPremisesId = await _premiseService.Update(premisesId, premises);


            if (updatedPremisesId == Guid.Empty)
            {
                return NotFound("You don't have this premises");
            }

            return Ok(updatedPremisesId);
        }

        [HttpPut("UpdateMainImage/{premisesId:guid}")]
        [Authorize]
        public async Task<ActionResult<Guid>> UpdateMainImage([FromRoute] Guid premisesId, [FromForm] UpdateMainImageRequest request)
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
            if (request.newImage == null)
                return BadRequest("The newImage field is required.");

            return await _premiseService.UpdateMainImage(premisesId, request.newImage);
        }
    }
}
