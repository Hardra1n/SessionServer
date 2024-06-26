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

    public Session CreateSession(SessionForCreationDto sessionDto)
    {
        return _repository.CreateSession(sessionDto.ToSession());
    }

    public Session GetSession(string sessionName)
    {
        var session = _repository.GetSession(sessionName);
        return session;
    }

    public void UpdateSession(string sessionName, SessionForUpdateDto sessionDto)
    {
        var sessionToUpdate = _repository.GetSession(sessionName);
        sessionToUpdate.Copy(sessionDto.ToSession());
    }
}