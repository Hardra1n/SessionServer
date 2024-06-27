using System.Collections.ObjectModel;

namespace Entities.Sessions;

public class Session : IClonable<Session>, IExpirable<Guid>
{
    private SessionState _sessionState;

    public Session(string name)
    {
        Name = name;
        CalculateExpirationToken();
    }


    public string Name { get; init; }
    public Guid ExpirationToken { get; private set; }
    public DateTime LastTimeModified { get; private set; } = DateTime.Now;
    public SessionState SessionState
    {
        get => _sessionState;
        set
        {
            _sessionState = value;
            ExecuteOnPropertyChange();
        }
    }


    private void ExecuteOnPropertyChange()
    {
        CalculateExpirationToken();
        LastTimeModified = DateTime.Now;
    }

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
        var session = new Session(Name)
        {
            ExpirationToken = ExpirationToken
        };
        session.Copy(this);
        return session;
    }
}