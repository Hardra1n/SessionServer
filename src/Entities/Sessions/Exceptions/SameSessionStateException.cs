using Entities.Exceptions;
using Entities.Sessions;

public class SameSessionStateException : BadRequestException
{
    public SameSessionStateException(SessionState sessionState)
        : base($"Session already has state: '{sessionState}'") { }
}