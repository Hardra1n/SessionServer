public class SessionPendingException : Exception
{
    public DateTime PendingUntil;
    public SessionPendingException(DateTime pendingUntil) : base($"Session can't be ready until {pendingUntil}")
    {
        PendingUntil = pendingUntil;
    }
}