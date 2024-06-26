namespace Entities.Sessions;

public class Session : IClonable<Session>, IExpirable<Guid>
{
    public Session(string name)
    {
        Name = name;
        CalculateExpirationToken();
    }

    public Session(string name, Guid expirationToken)
    {
        Name = name;
        ExpirationToken = expirationToken;
    }


    public Guid ExpirationToken { get; private set; }

    public string Name { get; init; }

    public SessionState SessionState { get; set; }


    public void CalculateExpirationToken() => ExpirationToken = Guid.NewGuid();

    public bool IsExpired(Guid token) => !ExpirationToken.Equals(token);

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        if (obj is not Session session)
        {
            return false;
        }
        return Name.Equals(session.Name, StringComparison.CurrentCultureIgnoreCase);
    }

    public override int GetHashCode()
    {
        return Name.ToLower().GetHashCode();
    }

    public void Copy(Session session)
    {
        ExpirationToken = session.ExpirationToken;
    }

    public Session Clone()
    {
        var session = new Session(Name, ExpirationToken);
        session.Copy(this);
        return session;
    }
}