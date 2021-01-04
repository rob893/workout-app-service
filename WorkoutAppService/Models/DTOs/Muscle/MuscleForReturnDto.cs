using WorkoutAppService.Entities;

namespace WorkoutAppService.Models.DTOs
{
    public record MuscleForReturnDto : IIdentifiable<int>
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
    }
}