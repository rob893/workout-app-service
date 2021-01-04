using System.ComponentModel.DataAnnotations;

namespace WorkoutAppService.Models.DTOs
{
    public record UpdateUserDto
    {
        [MaxLength(255)]
        public string? FirstName { get; init; }
        [MaxLength(255)]
        public string? LastName { get; init; }
    }
}