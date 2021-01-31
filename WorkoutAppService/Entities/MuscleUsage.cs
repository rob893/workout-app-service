using System.ComponentModel.DataAnnotations;

namespace WorkoutAppService.Entities
{
    public class MuscleUsage
    {
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; } = default!;
        public int MuscleId { get; set; }
        public Muscle Muscle { get; set; } = default!;
        [Range(0, 100)]
        public int Usage { get; set; }
    }
}