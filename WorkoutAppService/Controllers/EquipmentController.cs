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
    public class EquipmentController : ServiceControllerBase
    {
        private readonly EquipmentRepository equipmentRepository;
        private readonly IMapper mapper;


        public EquipmentController(EquipmentRepository equipmentRepository, IMapper mapper)
        {
            this.equipmentRepository = equipmentRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CursorPaginatedResponse<EquipmentForReturnDto>>> GetEquipmentAsync([FromQuery] EquipmentSearchParameters searchParams)
        {
            var equipment = await equipmentRepository.SearchAsync(searchParams);
            var paginatedResponse = CursorPaginatedResponse<EquipmentForReturnDto>.CreateFrom(equipment, mapper.Map<IEnumerable<EquipmentForReturnDto>>);

            return Ok(paginatedResponse);
        }

        [HttpGet("{id}", Name = "GetSingleEquipment")]
        public async Task<ActionResult<EquipmentForReturnDto>> GetSingleEquipmentAsync(int id)
        {
            var equipment = await equipmentRepository.GetByIdAsync(id);

            if (equipment == null)
            {
                return NotFound();
            }

            var equipmentToReturn = mapper.Map<EquipmentForReturnDto>(equipment);

            return Ok(equipmentToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentForReturnDto>> CreateEquipmentAsync([FromBody] EquipmentForCreationDto equipmentCreationDto)
        {
            var equipment = mapper.Map<Equipment>(equipmentCreationDto);

            equipmentRepository.Add(equipment);
            var saveResults = await equipmentRepository.SaveAllAsync();

            if (!saveResults)
            {
                return BadRequest("Failed to create the equipment.");
            }

            var eqReturn = mapper.Map<EquipmentForReturnDto>(equipment);

            return CreatedAtRoute("GetSingleEquipment", new { id = equipment.Id }, eqReturn);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSingleEquipmentAsync(int id)
        {
            var equipment = await equipmentRepository.GetByIdAsync(id);

            if (equipment == null)
            {
                return NotFound();
            }

            equipmentRepository.Delete(equipment);
            var saveResults = await equipmentRepository.SaveAllAsync();

            if (!saveResults)
            {
                return BadRequest("Failed to delete the equipment.");
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EquipmentForReturnDto>> PutEquipmentAsync(int id, [FromBody] EquipmentForCreationDto dto)
        {
            var equipment = await equipmentRepository.GetByIdAsync(id);

            if (equipment == null)
            {
                return NotFound();
            }

            this.mapper.Map(dto, equipment);

            var saveResult = await equipmentRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest("Could not apply changes.");
            }

            var equipmentToReturn = mapper.Map<EquipmentForReturnDto>(equipment);

            return Ok(equipmentToReturn);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<EquipmentForReturnDto>> UpdateEquipmentAsync(int id, [FromBody] JsonPatchDocument<EquipmentForUpdateDto> dtoPatchDoc)
        {
            if (dtoPatchDoc == null || dtoPatchDoc.Operations.Count == 0)
            {
                return BadRequest("A JSON patch document with at least 1 operation is required.");
            }

            if (!dtoPatchDoc.IsValid(out var errors))
            {
                return BadRequest(errors);
            }

            var equipment = await equipmentRepository.GetByIdAsync(id);

            if (equipment == null)
            {
                return NotFound();
            }

            var patchDoc = mapper.Map<JsonPatchDocument<Equipment>>(dtoPatchDoc);

            patchDoc.ApplyTo(equipment);

            var saveResult = await equipmentRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest("Could not apply changes.");
            }

            var equipmentToReturn = mapper.Map<EquipmentForReturnDto>(equipment);

            return Ok(equipmentToReturn);
        }
    }
}