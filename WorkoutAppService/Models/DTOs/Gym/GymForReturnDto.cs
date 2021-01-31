using System.Collections.Generic;
using WorkoutAppService.Entities;

namespace WorkoutAppService.Models.DTOs
{
    public record GymForReturnDto : IIdentifiable<int>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public bool IsPrimary { get; set; }
        public List<EquipmentForReturnDto> AvailableEquipment { get; set; } = new List<EquipmentForReturnDto>();
    }
}