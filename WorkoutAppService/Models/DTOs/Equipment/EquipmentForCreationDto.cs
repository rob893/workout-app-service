using System.ComponentModel.DataAnnotations;

namespace WorkoutAppService.Models.DTOs
{
    public record EquipmentForCreationDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; init; } = default!;
    }
}