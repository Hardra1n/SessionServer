using Entities.Sessions;
using Entities;
using Service.Contracts;

namespace Service;
public class SessionService : ISessionService
{
    private readonly ISessionRepository _repository;

    public SessionService(ISessionRepository sessionRepository)
    {
        _repository = sessionRepository;
    }

    public Session CreateSession(SessionForCreationDto session)
    {
        return _repository.CreateSession(session.ToSession());
    }

    public Session GetSession(string sessionName)
    {
        var session = _repository.GetSession(sessionName);
        return session;
    }
}