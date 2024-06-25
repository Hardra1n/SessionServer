using Entities.Exceptions;

namespace Entities.Sessions;

public class SessionAlreadyExistException : AlreadyExistException
{
    public SessionAlreadyExistException() : base("Session already exists.") { }
}