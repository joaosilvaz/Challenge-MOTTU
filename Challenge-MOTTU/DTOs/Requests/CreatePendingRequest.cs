namespace Challenge_MOTTU.DTOs.Requests
{
    public class CreatePendingRequest
    {
        public int UsuarioId { get; set; }
        public int BikeId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
