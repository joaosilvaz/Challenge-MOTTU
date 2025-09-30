namespace Challenge_MOTTU.DTOs.Responses
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public IDictionary<string, string> Links { get; set; } = new Dictionary<string, string>();
    }
}
