using System.Collections.Generic;
using WorkoutAppService.Entities;

namespace WorkoutAppService.Models.DTOs
{
    public record ExerciseForReturnDto : IIdentifiable<int>
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public int? PrimaryMuscleId { get; init; }
        public int? SecondaryMuscleId { get; init; }
        public List<ExerciseStepForReturnDto> ExerciseSteps { get; init; } = new List<ExerciseStepForReturnDto>();
    }
}