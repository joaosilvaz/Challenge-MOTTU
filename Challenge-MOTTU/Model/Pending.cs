using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Challenge_MOTTU.Enums;

namespace Challenge_MOTTU.Model
{
    public class Pending
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public StatusAluguel Status { get; set; } = StatusAluguel.Pendente;

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        [Required]
        public int BikeId { get; set; }

        [ForeignKey("BikeId")]
        public Bike Bike { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

    }
}   
