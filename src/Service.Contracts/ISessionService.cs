using Entities.Sessions;

namespace Service.Contracts;

public interface ISessionService
{
    Session GetSession(string sessionName);
    Session CreateSession(SessionForCreationDto session);
}