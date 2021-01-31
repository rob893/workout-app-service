using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkoutAppService.Entities
{
    public class Muscle : IIdentifiable<int>
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}