using Gymbokning.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gymbokning.Areas.Identity
{
    public class ApplicationClaimsPrincipalFactory:UserClaimsPrincipalFactory<ApplicationUser>
    {
        public ApplicationClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> options):base(userManager,options)
        {
            
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        { 
            var identity=await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim("FullName", user.FullName));

            return identity;
        }
    }
}
