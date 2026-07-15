using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace api.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName {get; set;} = string.Empty;
        public ICollection<UserClaim> Claims {get; set;} = new List<UserClaim>();
        public ICollection<UserRole> UserRoles {get; set;} = new List<UserRole>();
        public ICollection<UserLogin> UserLogins {get; set;} = new List<UserLogin>();
        public ICollection<UserToken> UserTokens {get; set;} = new List<UserToken>();
    }
}
