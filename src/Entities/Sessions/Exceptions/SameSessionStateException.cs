using Entities.Sessions;

public class SameSessionStateException : Exception
{
    public SameSessionStateException(SessionState sessionState)
        : base($"Session already has state: '{sessionState}'") { }
}