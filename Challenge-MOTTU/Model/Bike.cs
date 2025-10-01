using System.ComponentModel.DataAnnotations;

namespace Challenge_MOTTU.Model
{
    public class Bike
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Modelo { get; set; } = string.Empty;

        [Required]
        [MaxLength(7)]
        public string Placa { get; set; } = string.Empty;

        [Required]
        [MaxLength(17)]
        public string Chassi { get; set; }

        public int Ano { get; set; }

        public bool Disponivel { get; set; } 

        public ICollection<Pending> Pendings { get; set; }
    }
}
