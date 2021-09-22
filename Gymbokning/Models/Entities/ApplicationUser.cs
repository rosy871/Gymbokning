using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gymbokning.Models.Entities
{
    public class ApplicationUser:IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get   { return $"{FirstName} {LastName}"; } }
        public DateTime TimeOfRegistration { get; set; }



        // public ICollection<GymClass> GymClasses { get; set; }
        public ICollection<ApplicationUserGymclass> AttendedClasses { get; set; }

       

    }
}
