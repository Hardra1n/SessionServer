namespace Entities.Sessions;

public class Session
{
    public string Name { get; set; } = string.Empty;
    public SessionState SessionState { get; set; }

    public Session(string name)
    {
        Name = name;
    }

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
}