namespace Challenge_MOTTU.Exceptions
{
    public class BikeNotFoundException : Exception
    {

        private const string MENSAGEM_PADRAO = "Bike não encontrada";
        
        public BikeNotFoundException() : base(MENSAGEM_PADRAO) { }

        public BikeNotFoundException(string message) : base(message){}

       public BikeNotFoundException(string message, Exception innerException) : base(message, innerException){}
    }
}
