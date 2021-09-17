using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gymbokning.Models.Entities
{
    public class ApplicationUserGymclass
    {
        public int Id { get; set; }

        public int GymClassId { get; set; }
        public GymClass GymClass { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
