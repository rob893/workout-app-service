namespace WorkoutAppService.Models.DTOs
{
    public record ExerciseStepForReturnDto
    {
        public int ExerciseStepNumber { get; init; }
        public string Description { get; init; } = default!;
    }
}