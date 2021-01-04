using System.ComponentModel.DataAnnotations;

namespace WorkoutAppService.Models.DTOs
{
    public record ExerciseCategoryForCreationDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; init; } = default!;
    }
}