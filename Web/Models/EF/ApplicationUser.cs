using Microsoft.AspNetCore.Identity;
using System;

namespace Web.Models.EF
{
    public class ApplicationUser : IdentityUser
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
