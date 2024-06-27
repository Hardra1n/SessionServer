using Entities.Sessions;

namespace Service.Contracts;

public interface ISessionService
{
    SessionForOutDto GetSession(string sessionName);
    SessionForOutDto CreateSession(SessionForCreationDto session);
    void UpdateSession(string sessionName, SessionForUpdateDto session);
}