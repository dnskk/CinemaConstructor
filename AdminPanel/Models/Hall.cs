using System;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class Hall
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public bool Is3D { get; set; }

        public bool IsImax { get; set; }

        public string HallTableJson { get; set; }

        public Cinema Cinema { get; set; }
    }
}
