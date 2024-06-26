using Entities.Sessions;

public record SessionForUpdateDto(Guid ExpirationToken, SessionState SessionState);