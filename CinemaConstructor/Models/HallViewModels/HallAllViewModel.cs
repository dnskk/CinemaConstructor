using System.Collections.Generic;
using System.Linq;
using CinemaConstructor.Database.Entities;

namespace CinemaConstructor.Models.HallViewModels
{
    public class HallAllViewModel
    {
        public List<IGrouping<string, Hall>> GroupedHalls { get; set; }
    }
}
