using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkoutAppService.Models.DTOs
{
    public record GymForCreateDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
        public bool IsPrimary { get; set; }
        public List<EquipmentForReturnDto> AvailableEquipment { get; set; } = new List<EquipmentForReturnDto>();
    }
}