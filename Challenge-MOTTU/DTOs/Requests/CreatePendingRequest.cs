namespace Challenge_MOTTU.DTOs.Requests
{
    public class CreatePendingRequest
    {
        public int UsuarioId { get; set; }
        public int BikeId { get; set; }
        public DateTimeOffset DataInicio { get; set; }
        public DateTimeOffset DataFim { get; set; }
    }
}
