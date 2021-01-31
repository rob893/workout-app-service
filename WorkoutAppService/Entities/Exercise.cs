using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkoutAppService.Entities
{
    public class Exercise : IIdentifiable<int>
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public int? PrimaryMuscleId { get; set; }
        public int? SecondaryMuscleId { get; set; }
        public List<MuscleUsage> Muscles { get; set; } = new List<MuscleUsage>();
        public List<ExerciseStep> ExerciseSteps { get; set; } = new List<ExerciseStep>();
        public List<Equipment> Equipment { get; set; } = new List<Equipment>();
        public List<ExerciseCategory> ExerciseCategorys { get; set; } = new List<ExerciseCategory>();
    }
}