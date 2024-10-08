using Entities.Exceptions;

namespace Entities.Sessions;

public class Session : IClonable<Session>, IExpirable<Guid>
{
    private SessionState _sessionState;
    private int _numberOfUsers;

    public Session(string name) : this(name, 1) { }
    public Session(string name, int numberOfUsers)
    {
        Name = name;
        NumberOfUsers = numberOfUsers;
        CalculateExpirationToken();
    }


    public string Name { get; init; }
    public int NumberOfUsers
    {
        get => _numberOfUsers;
        init
        {
            if (value <= 0)
                throw new BadRequestException("Number of users must be greater then 0");
            _numberOfUsers = value;
        }
    }
    public Guid ExpirationToken { get; private set; }
    public DateTime LastTimeModified { get; private set; } = DateTime.Now;
    public virtual SessionState SessionState
    {
        get => _sessionState;
        set
        {
            _sessionState = value;
            ExecuteOnPropertyChange();
        }
    }


    protected void ExecuteOnPropertyChange()
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
        SessionState = session.SessionState;
    }

    public Session Clone()
    {
        var session = new Session(Name, NumberOfUsers);
        session.Copy(this);
        session.ExpirationToken = ExpirationToken;
        return session;
    }
}