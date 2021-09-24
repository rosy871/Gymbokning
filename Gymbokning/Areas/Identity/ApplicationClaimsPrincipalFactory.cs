using Gymbokning.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gymbokning.Areas.Identity
{
    public class ApplicationClaimsPrincipalFactory:IClaimsTransformation
    {
        private readonly UserManager<ApplicationUser> userManager;

        //ublic ApplicationClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> options):base(userManager,options)
        //{

        //}

        public ApplicationClaimsPrincipalFactory(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }


        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var temp = principal.Clone();
            var newIdentity = (ClaimsIdentity)temp.Identity;

            var id = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id is null) return principal;

            var user = await userManager.FindByIdAsync(id);

            if (user is null) return principal;

            var claim = new Claim("FullName", user.FullName);
            newIdentity.AddClaim(claim);

           // var claidentity = new ClaimsPrincipal(newIdentity);

            return temp;
        }

        
    }
}
