using Entities.Sessions;
using Entities;
using Service.Contracts;
using System.Collections.Concurrent;

namespace Service;
public class SessionService : ISessionService
{
    private readonly ISessionRepository _repository;

    public SessionService(ISessionRepository sessionRepository)
    {
        _repository = sessionRepository;
    }

    public SessionForOutDto CreateSession(SessionForCreationDto sessionDto)
    {
        var session = _repository.CreateSession(sessionDto.ToSession());
        return session.ToSessionForOutDto();
    }

    public SessionForOutDto GetSession(string sessionName)
    {
        var session = _repository.GetSession(sessionName);
        return session.ToSessionForOutDto();
    }

    public void UpdateSession(string sessionName, SessionForUpdateDto sessionDto)
    {
        _repository.UpdateSession(sessionDto.ToSession(sessionName));
    }
}