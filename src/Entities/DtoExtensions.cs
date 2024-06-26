using Entities.Sessions;

namespace Entities;

public static class DtoExtensions
{
    public static Session ToSession(this SessionForCreationDto session)
    {
        return new Session(session.Name);
    }

    public static Session ToSession(this SessionForUpdateDto session, string sessionName)
    {
        return new Session(sessionName, session.ExpirationToken)
        {
            SessionState = session.SessionState
        };
    }
}