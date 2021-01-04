using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutAppService.Data.Repositories;
using WorkoutAppService.Models.Responses;
using WorkoutAppService.Models.DTOs;
using WorkoutAppService.Models.QueryParameters;

namespace WorkoutAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ServiceControllerBase
    {
        private readonly ExerciseRepository exerciseRepository;
        private readonly IMapper mapper;


        public ExercisesController(ExerciseRepository exerciseRepository, IMapper mapper)
        {
            this.exerciseRepository = exerciseRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CursorPaginatedResponse<ExerciseForReturnDto>>> GetExercisesAsync([FromQuery] ExerciseSearchParameters searchParams)
        {
            var exercises = await exerciseRepository.SearchAsync(searchParams);
            var paginatedResponse = CursorPaginatedResponse<ExerciseForReturnDto>.CreateFrom(exercises, mapper.Map<IEnumerable<ExerciseForReturnDto>>);

            return Ok(paginatedResponse);
        }

        [HttpGet("{id}", Name = "GetExerciseAsync")]
        public async Task<ActionResult<ExerciseForReturnDto>> GetExerciseAsync(int id)
        {
            var exercise = await exerciseRepository.GetByIdAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            var exerciseToReturn = mapper.Map<ExerciseForReturnDto>(exercise);

            return Ok(exerciseToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExerciseAsync(int id)
        {
            var exercise = await exerciseRepository.GetByIdAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            exerciseRepository.Delete(exercise);
            var saveResult = await exerciseRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest("Unable to delete exercise.");
            }

            return NoContent();
        }
    }
}