namespace ProEventos.Application.Exceptions;

public class LoteDataInvalidaException : Exception
{
    public LoteDataInvalidaException(string message) : base(message)
    {
    }
}