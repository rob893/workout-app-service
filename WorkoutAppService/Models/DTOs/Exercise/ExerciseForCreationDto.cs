using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkoutAppService.Entities;

namespace WorkoutAppService.Models.DTOs
{
    public record ExerciseForCreationDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; init; } = default!;
        public int? PrimaryMuscleId { get; init; }
        public int? SecondaryMuscleId { get; init; }
        [Required]
        public List<ExerciseStep> ExerciseSteps { get; init; } = new List<ExerciseStep>();
        public List<int> EquipmentIds { get; init; } = new List<int>();
        [Required]
        public List<int> ExerciseCategoryIds { get; init; } = new List<int>();
    }
}