using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models.Identity
{
    public class Role : IdentityRole
    {
        public ICollection<UserRole> UserRoles {get; set;} = new List<UserRole>();
        public ICollection<RoleClaim> RoleClaims {get; set;} = new List<RoleClaim>();
    }
}
