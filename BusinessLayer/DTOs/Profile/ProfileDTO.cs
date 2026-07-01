using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Profile
{
    public sealed class MyProfileResponse
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string Role { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }

        public int? TutorId { get; set; }
        public string? TutorBio { get; set; }
    }

    public sealed class UpdateMyProfileRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }

        // Tutor only
        public string? TutorBio { get; set; }
    }

}
