﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gymbokning.Models.Entities
{
    public class GymClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:hh\:mm\:ss}")]
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [Required, DataType(DataType.Time)]
        public TimeSpan Duration { get; set; }
        public DateTime EndTime { get { return StartTime + Duration; } }
        public String Description { get; set; }

        public ICollection<ApplicationUserGymclass> AttendingMember { get; set;  }
      //sp  public ICollection<ApplicationUser> ApplicationUsers { get; set; }

    }
}
