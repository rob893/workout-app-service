namespace WorkoutAppService.Models.DTOs
{
    public record MuscleForUpdateDto
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
    }
}