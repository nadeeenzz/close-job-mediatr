namespace Recruitment.Application.Exceptions;

// Thrown when an entity does not exist -> API returns 404
public class NotFoundException : Exception
{
    public NotFoundException(string message, object id) : base(message) { }
}
