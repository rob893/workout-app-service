using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutAppService.Data.Repositories;
using WorkoutAppService.Models.QueryParameters;
using WorkoutAppService.Models.DTOs;
using WorkoutAppService.Models.Responses;
using WorkoutAppService.Entities;
using WorkoutAppService.Extensions;

namespace WorkoutAppService.Controllers
{
    [Route("api/users/{userId}/gyms")]
    [ApiController]
    public class UserGymsController : ServiceControllerBase
    {
        private readonly GymRepository gymRepository;
        private readonly IMapper mapper;


        public UserGymsController(GymRepository gymRepository, IMapper mapper)
        {
            this.gymRepository = gymRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CursorPaginatedResponse<GymForReturnDto>>> GetUserGymsAsync([FromRoute] int userId, [FromQuery] CursorPaginationParameters searchParams)
        {
            if (!IsUserAuthorizedForResource(userId))
            {
                return Forbidden("You can only access your own gyms.");
            }

            var gymSearchParams = this.mapper.Map<GymSearchParameters>(searchParams);
            gymSearchParams.UserId = userId;

            var gyms = await gymRepository.SearchAsync(gymSearchParams);
            var paginatedResponse = CursorPaginatedResponse<GymForReturnDto>.CreateFrom(gyms, mapper.Map<IEnumerable<GymForReturnDto>>, gymSearchParams);

            return Ok(paginatedResponse);
        }

        [HttpGet("{id}", Name = "GetUserGym")]
        public async Task<ActionResult<GymForReturnDto>> GetUserGymAsync([FromRoute] int userId, [FromRoute] int id)
        {
            if (!IsUserAuthorizedForResource(userId))
            {
                return Forbidden("You can only access your own gyms.");
            }

            var gym = await gymRepository.GetByIdAsync(id);

            if (gym == null)
            {
                return NotFound($"Muscle with id {id} does not exist.");
            }

            if (!this.IsUserAuthorizedForResource(gym))
            {
                return Forbid("User is not authorized to see this gym.");
            }

            var gymToReturn = mapper.Map<GymForReturnDto>(gym);

            return Ok(gymToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<GymForReturnDto>> CreateUserGymAsync([FromRoute] int userId, [FromBody] GymForCreateDto createDto)
        {
            if (!IsUserAuthorizedForResource(userId))
            {
                return Forbidden("You can only create gyms for yourself.");
            }

            var newGym = mapper.Map<Gym>(createDto);
            newGym.UserId = userId;

            gymRepository.Add(newGym);

            var saveResult = await gymRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest("Unable to create gym.");
            }

            var gymToReturn = mapper.Map<GymForReturnDto>(newGym);

            return CreatedAtRoute("GetUserGym", new { userId, id = newGym.Id }, gymToReturn);
        }
    }
}