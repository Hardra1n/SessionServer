using Entities.Sessions;

public class SessionRepository : ISessionRepository
{
    private object _locker = new object();
    private HashSet<Session> _sessions;

    public SessionRepository()
    {
        _sessions = new HashSet<Session>();
    }

    public Session CreateSession(Session session)
    {
        lock (_locker)
        {
            if (!_sessions.Add(session))
                throw new SessionAlreadyExistException();
            return session;
        }
    }

    public Session GetSession(string sessionName)
    {
        if (!_sessions.TryGetValue(new Session(sessionName), out Session? session)
           || session == null)
        {
            throw new SessionNotFoundException(sessionName);
        }
        return session;
    }
}

