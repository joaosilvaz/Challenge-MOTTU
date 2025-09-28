namespace Challenge_MOTTU.Exceptions
{
    public class UsuarioNotFoundException : Exception
    {
        private const string MENSAGEM_PADRAO = "Usuário não encontrado";

        public UsuarioNotFoundException() : base(MENSAGEM_PADRAO) {}

        public UsuarioNotFoundException(string message) : base(message) {}

        public UsuarioNotFoundException(string message, Exception innerException) : base(message, innerException){}
    }
}
