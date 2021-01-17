using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WorkoutAppService.Data.Repositories;
using WorkoutAppService.Entities;
using WorkoutAppService.Extensions;
using WorkoutAppService.Models.DTOs;
using WorkoutAppService.Models.QueryParameters;
using WorkoutAppService.Models.Responses;

namespace WorkoutAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusclesController : ServiceControllerBase
    {
        private readonly MuscleRepository muscleRepository;
        private readonly IMapper mapper;


        public MusclesController(MuscleRepository muscleRepository, IMapper mapper)
        {
            this.muscleRepository = muscleRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CursorPaginatedResponse<MuscleForReturnDto>>> GetMusclesAsync([FromQuery] CursorPaginationParameters searchParams)
        {
            var muscles = await muscleRepository.SearchAsync(searchParams);
            var paginatedResponse = CursorPaginatedResponse<MuscleForReturnDto>.CreateFrom(muscles, mapper.Map<IEnumerable<MuscleForReturnDto>>);

            return Ok(paginatedResponse);
        }

        [HttpGet("{id}", Name = "GetMuscle")]
        public async Task<ActionResult<MuscleForReturnDto>> GetMuscleAsync(int id)
        {
            var muscle = await muscleRepository.GetByIdAsync(id);

            if (muscle == null)
            {
                return NotFound($"Muscle with id {id} does not exist.");
            }

            var muscleToReturn = mapper.Map<MuscleForReturnDto>(muscle);

            return Ok(muscleToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<MuscleForReturnDto>> CreateMuscleAsync([FromBody] MuscleForCreateDto muscleForCreate)
        {
            var newMuscle = mapper.Map<Muscle>(muscleForCreate);
            muscleRepository.Add(newMuscle);

            var saveResult = await muscleRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest("Unable to create muscle.");
            }

            var muscleToReturn = mapper.Map<MuscleForReturnDto>(newMuscle);

            return CreatedAtRoute("GetMuscle", new { id = newMuscle.Id }, muscleToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMuscleAsync(int id)
        {
            var muscle = await muscleRepository.GetByIdAsync(id);

            if (muscle == null)
            {
                return NotFound();
            }

            muscleRepository.Delete(muscle);

            if (!await muscleRepository.SaveAllAsync())
            {
                return BadRequest("Failed to delete the muscle.");
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MuscleForReturnDto>> PutMuscleAsync(int id, [FromBody] MuscleForCreateDto dto)
        {
            var muscle = await muscleRepository.GetByIdAsync(id);

            if (muscle == null)
            {
                return NotFound();
            }

            mapper.Map(dto, muscle);

            var saveResult = await muscleRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest("Could not apply changes.");
            }

            var muscleToReturn = mapper.Map<MuscleForReturnDto>(muscle);

            return Ok(muscleToReturn);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<MuscleForReturnDto>> UpdateMuscleAsync(int id, [FromBody] JsonPatchDocument<MuscleForUpdateDto> dtoPatchDoc)
        {
            if (dtoPatchDoc == null || dtoPatchDoc.Operations.Count == 0)
            {
                return BadRequest("A JSON patch document with at least 1 operation is required.");
            }

            if (!dtoPatchDoc.IsValid(out var errors))
            {
                return BadRequest(errors);
            }

            var muscle = await muscleRepository.GetByIdAsync(id);

            if (muscle == null)
            {
                return NotFound();
            }

            var patchDoc = mapper.Map<JsonPatchDocument<Muscle>>(dtoPatchDoc);

            patchDoc.ApplyTo(muscle);

            var saveResult = await muscleRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest("Could not apply changes.");
            }

            var muscleToReturn = mapper.Map<MuscleForReturnDto>(muscle);

            return Ok(muscleToReturn);
        }
    }
}