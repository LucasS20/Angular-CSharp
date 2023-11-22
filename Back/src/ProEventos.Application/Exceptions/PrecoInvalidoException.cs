namespace ProEventos.Application.Exceptions;

public class PrecoInvalidoException : Exception
{
    public PrecoInvalidoException(string message) : base(message)
    {
    }
}