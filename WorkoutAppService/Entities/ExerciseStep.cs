namespace WorkoutAppService.Entities
{
    public class ExerciseStep
    {
        public Exercise Exercise { get; set; } = default!;
        public int ExerciseId { get; set; }
        public int ExerciseStepNumber { get; set; }
        public string Description { get; set; } = default!;
    }
}