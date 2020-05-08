using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.LandingViewModels
{
    public class PersonalAreaViewModel
    {
        [Required]
        public string Name { get; set; }

        public List<Company> Companies { get; set; }

        public bool IsNeedToClear { get; set; }
    }
}
