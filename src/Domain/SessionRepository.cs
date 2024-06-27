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

    public void DeleteSession(Session session)
    {
        lock (_locker)
        {
            var sessionToDelete = CheckIfSessionExistsAndNotNull(session);

            if (sessionToDelete.IsExpired(session.ExpirationToken))
                throw new SessionExpiredException(session.ExpirationToken);
            if (_sessions.Remove(session))
                throw new SessionNotFoundException(session.Name);
        }
    }

    public Session GetSession(string sessionName)
    {
        lock (_locker)
        {
            var session = CheckIfSessionExistsAndNotNull(new Session(sessionName));

            return session.Clone();
        }
    }

    public Session UpdateSession(Session updateSession)
    {
        lock (_locker)
        {
            var session = CheckIfSessionExistsAndNotNull(updateSession);

            if (session.IsExpired(updateSession.ExpirationToken))
            {
                throw new SessionExpiredException(updateSession.ExpirationToken);
            }
            session.Copy(updateSession);
            session.CalculateExpirationToken();

            return session.Clone();
        }
    }

    private Session CheckIfSessionExistsAndNotNull(Session session)
    {
        if (session == null)
        {
            throw new SessionNullPointerException();
        }

        if (!_sessions.TryGetValue(session, out Session? sessionToReturn))
        {
            throw new SessionNotFoundException(session.Name);
        }
        return sessionToReturn;
    }
}

