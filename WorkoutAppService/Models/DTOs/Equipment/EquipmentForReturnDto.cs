using WorkoutAppService.Entities;

namespace WorkoutAppService.Models.DTOs
{
    public record EquipmentForReturnDto : IIdentifiable<int>
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
    }
}