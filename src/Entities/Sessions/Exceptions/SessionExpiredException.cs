public class SessionExpiredException : BadRequestException
{
    public SessionExpiredException(Guid guid)
        : base($"Session expired with expiration token: {guid}") { }
}
