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
            return session.Clone();
        }
    }

    public Session GetSession(string sessionName)
    {
        lock (_locker)
        {
            if (!_sessions.TryGetValue(new Session(sessionName), out Session? session)
               || session == null)
            {
                throw new SessionNotFoundException(sessionName);
            }

            return session.Clone();
        }
    }

    public Session UpdateSession(Session updateSession)
    {
        lock (_locker)
        {
            if (!_sessions.TryGetValue(updateSession, out Session? session)
               || session == null)
            {
                throw new SessionNotFoundException(updateSession.Name);
            }

            if (session.IsExpired(updateSession.ExpirationToken))
            {
                throw new SessionExpiredException(updateSession.ExpirationToken);
            }
            session.Copy(updateSession);
            session.CalculateExpirationToken();

            return session.Clone();
        }

    }
}

