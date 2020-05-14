using System.Collections.Generic;
using System.Linq;
using CinemaConstructor.Entities;

namespace CinemaConstructor.Models.HallViewModels
{
    public class HallAllViewModel
    {
        public List<IGrouping<string, Hall>> GroupedHalls { get; set; }
    }
}
