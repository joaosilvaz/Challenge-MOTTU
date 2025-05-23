using System.Runtime.ConstrainedExecution;

namespace Challenge_MOTTU.Exceptions
{
    public class UsuarioException : Exception
    {
        private const string MENSAGEM_PADRAO = "Usuário não encontrado";

        public UsuarioException(string? message = MENSAGEM_PADRAO) : base(message)
        {
        }
    }
}
