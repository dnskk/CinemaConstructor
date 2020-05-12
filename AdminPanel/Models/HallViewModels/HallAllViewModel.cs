using System.Collections.Generic;
using System.Linq;

namespace AdminPanel.Models.HallViewModels
{
    public class HallAllViewModel
    {
        public List<IGrouping<string, Hall>> GroupedHalls { get; set; }
    }
}
