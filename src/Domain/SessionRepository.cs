using System.Collections.ObjectModel;
using Entities.Sessions;

public class SessionRepository : ISessionRepository
{
    private object _locker = new object();
    private HashSet<Session> _sessions;

    public SessionRepository()
    {
        _sessions = new HashSet<Session>();
    }


    public IReadOnlyCollection<Session> GetAllSessions()
    {
        lock (_locker)
        {
            return new ReadOnlyCollection<Session>(_sessions.ToList());
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

    public Session CreateSession(Session session)
    {
        lock (_locker)
        {
            if (!_sessions.Add(session))
                throw new SessionAlreadyExistException();
            return session.Clone();
        }
    }

    public Session UpdateSession(Guid expirationToken, Session updateSession)
    {
        lock (_locker)
        {
            var session = CheckIfSessionExistsAndNotNull(updateSession);
            CheckIfSessionExpired(expirationToken, updateSession);

            session.Copy(updateSession);

            return session.Clone();

        }
    }

    public void DeleteSession(Guid expirationToken, Session session)
    {
        lock (_locker)
        {
            var sessionToDelete = CheckIfSessionExistsAndNotNull(session);
            CheckIfSessionExpired(expirationToken, sessionToDelete);

            if (_sessions.Remove(session))
                throw new SessionNotFoundException(session.Name);
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

    private void CheckIfSessionExpired(Guid expirationToken, Session session)
    {
        if (session.IsExpired(expirationToken))
            throw new SessionExpiredException(expirationToken);
    }
}

