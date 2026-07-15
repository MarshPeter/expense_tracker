using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models.Identity
{
    public class UserRole : IdentityUserRole<string>
    {
        public ApplicationUser User { get; set; } = null!;
        public Role Role {get; set;} = null!;
    }
}
