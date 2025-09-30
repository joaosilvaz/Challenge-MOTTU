namespace Challenge_MOTTU.DTOs.Responses
{
    public class BikeResponse
    {
        public int Id { get; set; }
        public string Modelo { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public int Ano { get; set; }
        public bool Disponivel { get; set; }
    }
}
