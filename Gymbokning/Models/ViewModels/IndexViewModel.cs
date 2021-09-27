using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gymbokning.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<NewIndexViewModel> GymClasses { get; set; }
        public bool ShowHistory { get; set; }
    }
}
