namespace Recruitment.Application.Exceptions;

// Thrown when a business rule is broken -> API returns 400
public class BusinessRuleException : Exception
{
    public BusinessRuleException(string message) : base(message) { }
}
