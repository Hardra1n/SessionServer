using Entities.Exceptions;

namespace Entities.Sessions;

public class SessionNotFoundException : NotFoundException
{
    public SessionNotFoundException(string sessionName)
        : base($"Session with name {sessionName} not found.") { }
}