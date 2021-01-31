using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkoutAppService.Entities
{
    public class Equipment : IIdentifiable<int>
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; } = default!;
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
        public List<Gym> Gyms { get; set; } = new List<Gym>();
    }
}