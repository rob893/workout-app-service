using System.ComponentModel.DataAnnotations;

namespace WorkoutAppService.Models.DTOs
{
    public record LoginGoogleUserDto
    {
        [Required]
        public string IdToken { get; init; } = default!;
        [Required]
        public string DeviceId { get; init; } = default!;
    }
}