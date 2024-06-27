using Entities.Exceptions;

namespace Entities.Sessions;

public class SessionExpiredException : BadRequestException
{
    public SessionExpiredException(Guid guid)
        : base($"Session expired with expiration token: {guid}") { }
}
