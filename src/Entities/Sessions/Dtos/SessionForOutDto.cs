using Entities.Sessions;

public record SessionForOutDto(string Name, Guid ExpirationToken, SessionState SessionState);