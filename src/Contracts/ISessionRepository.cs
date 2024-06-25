using Entities.Sessions;

public interface ISessionRepository
{
    Session CreateSession(Session session);
    Session GetSession(string sessionName);
}