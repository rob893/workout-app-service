using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WorkoutAppService.Entities
{
    public class Role : IdentityRole<int>, IIdentifiable<int>
    {
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}