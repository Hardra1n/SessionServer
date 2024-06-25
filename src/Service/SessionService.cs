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
        throw new NotImplementedException();
    }

    public Session GetSession(string sessionName)
    {
        throw new NotImplementedException();
    }
}