namespace Challenge_MOTTU.Exceptions
{
    public class PendingNotFoundException : Exception
    {
        private const string MENSAGEM_PADRAO = "Pendência não encontrada";

        public PendingNotFoundException() : base(MENSAGEM_PADRAO) { }

        public PendingNotFoundException(string message) : base(message) { }

        public PendingNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
