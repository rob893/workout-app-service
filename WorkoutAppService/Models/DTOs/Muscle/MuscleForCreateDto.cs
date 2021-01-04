using System.ComponentModel.DataAnnotations;

namespace WorkoutAppService.Models.DTOs
{
    public record MuscleForCreateDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
    }
}