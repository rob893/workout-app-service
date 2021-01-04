using System;

namespace WorkoutAppService.Models.DTOs
{
    public record EditRoleDto
    {
        public string[] RoleNames { get; init; } = Array.Empty<string>();
    }
}