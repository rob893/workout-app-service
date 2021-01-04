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
    public class ExerciseCategoriesController : ServiceControllerBase
    {
        private readonly ExerciseCategoryRepository exerciseCategoryRepository;
        private readonly IMapper mapper;


        public ExerciseCategoriesController(ExerciseCategoryRepository exerciseCategoryRepository, IMapper mapper)
        {
            this.exerciseCategoryRepository = exerciseCategoryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CursorPaginatedResponse<ExerciseCategoryForReturnDto>>> GetExerciseCategoriesAsync([FromQuery] ExerciseCategorySearchParameters searchParams)
        {
            var categories = await exerciseCategoryRepository.SearchAsync(searchParams);
            var paginatedResponse = CursorPaginatedResponse<ExerciseCategoryForReturnDto>.CreateFrom(categories, mapper.Map<IEnumerable<ExerciseCategoryForReturnDto>>);

            return Ok(paginatedResponse);
        }

        [HttpGet("{id}", Name = "GetExerciseCategoryAsync")]
        public async Task<ActionResult<ExerciseCategoryForReturnDto>> GetExerciseCategoryAsync(int id)
        {
            var category = await exerciseCategoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound($"Exercise category with id {id} does not exist.");
            }

            var categoryForReturnDto = mapper.Map<ExerciseCategoryForReturnDto>(category);

            return Ok(categoryForReturnDto);
        }

        [HttpPost]
        public async Task<ActionResult<ExerciseCategoryForReturnDto>> CreateExerciseCategoryAsync([FromBody] ExerciseCategoryForCreationDto categoryForCreation)
        {
            var newCategory = mapper.Map<ExerciseCategory>(categoryForCreation);
            exerciseCategoryRepository.Add(newCategory);

            var saveResult = await exerciseCategoryRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest("Unable to create category.");
            }

            var categoryForReturn = mapper.Map<ExerciseCategoryForReturnDto>(newCategory);

            return CreatedAtRoute("GetExerciseCategoryAsync", new { id = newCategory.Id }, categoryForReturn);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExerciseCategoryAsync(int id)
        {
            var category = await exerciseCategoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            exerciseCategoryRepository.Delete(category);
            var saveResult = await exerciseCategoryRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest("Failed to delete the category.");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ExerciseCategoryForReturnDto>> UpdateExerciseCategoryAsync(int id, [FromBody] JsonPatchDocument<ExerciseCategoryForUpdateDto> dtoPatchDoc)
        {
            if (dtoPatchDoc == null || dtoPatchDoc.Operations.Count == 0)
            {
                return BadRequest("A JSON patch document with at least 1 operation is required.");
            }

            if (!dtoPatchDoc.IsValid(out var errors))
            {
                return BadRequest(errors);
            }

            var category = await exerciseCategoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var patchDoc = mapper.Map<JsonPatchDocument<ExerciseCategory>>(dtoPatchDoc);

            patchDoc.ApplyTo(category);

            var saveResult = await exerciseCategoryRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest("Could not apply changes.");
            }

            var categoryToReturn = mapper.Map<ExerciseCategoryForReturnDto>(category);

            return Ok(categoryToReturn);
        }
    }
}