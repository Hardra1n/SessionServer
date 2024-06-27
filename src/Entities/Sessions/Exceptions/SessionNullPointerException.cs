using Entities.Exceptions;

namespace Entities.Sessions;

public class SessionNullPointerException : BadRequestException
{
    public SessionNullPointerException()
    : base("Session is null") { }
}
