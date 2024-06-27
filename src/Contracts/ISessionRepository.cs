using Entities.Sessions;

namespace Contracts;

public interface ISessionRepository
{
    Session CreateSession(Session session);
    Session GetSession(string sessionName);
    Session UpdateSession(Guid expirationToken, Session updateSession);
    void DeleteSession(Guid expirationToken, Session session);
    IReadOnlyCollection<Session> GetAllSessions();
}