using System.Collections.Generic;
using WorkoutAppService.Models.DTOs;

namespace WorkoutAppService.Entities
{
    public class Gym : IIdentifiable<int>, IOwnedByUser<int>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public bool IsPrimary { get; set; }
        public List<Equipment> AvailableEquipment { get; set; } = new List<Equipment>();
    }
}