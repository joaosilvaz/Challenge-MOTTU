using System.ComponentModel.DataAnnotations;

namespace Challenge_MOTTU.DTOs.Requests
{
    public class CreateBikeRequest
    {
        public string Modelo { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public string Chassi { get; set; }
        public int Ano { get; set; }
    }
}
