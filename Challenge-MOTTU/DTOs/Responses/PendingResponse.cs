
namespace Challenge_MOTTU.DTOs.Responses
{
    public class PendingResponse
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTimeOffset DataInicio { get; set; }
        public DateTimeOffset DataFim { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioResponse? Usuario { get; set; }

        public int BikeId { get; set; }
        public BikeResponse? Bike { get; set; }
        public Dictionary<string, string> Links { get; internal set; }
    }
}
