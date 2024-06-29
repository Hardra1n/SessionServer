using Entities.Sessions;

namespace Entities;

public static class DtoExtensions
{
    public static Session ToSession(this SessionForCreationDto session)
    {
        return new SearchSession(session.Name, session.NumberOfUsers, TimeSpan.FromSeconds(session.SearchingPeriodSeconds));
    }

    public static Session ToSession(this SessionForUpdateDto session, string sessionName)
    {
        return new Session(sessionName)
        {
            SessionState = session.SessionState
        };
    }

    public static SessionForOutDto ToSessionForOutDto(this Session session)
    {
        return new SessionForOutDto(session.Name, session.ExpirationToken, session.SessionState);
    }
}