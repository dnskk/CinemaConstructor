using System.ComponentModel.DataAnnotations;

namespace CinemaConstructor.Entities
{
    public class Hall
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public bool Is3D { get; set; }

        public bool IsImax { get; set; }

        public int Seats { get; set; }

        public string HallTableJson { get; set; }

        public Cinema Cinema { get; set; }
    }
}
