using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models.Identity
{
    public class UserLogin : IdentityUserLogin<string>
    {
        public ApplicationUser User { get; set; } = null!;
    }
}
