using Entities.Sessions;

public class SearchSession : Session
{
    public SearchSession(string name, int numberOfUsers, TimeSpan searchingPeriod)
        : base(name, numberOfUsers)
    {
        SearchingPeriod = searchingPeriod;
    }
    public override SessionState SessionState
    {
        get => base.SessionState;
        set
        {
            if (SessionState == SessionState.Pending && DateTime.Now < PendingUntil)
                throw new SessionPendingException(PendingUntil);
            if (base.SessionState == value)
                throw new SameSessionStateException(SessionState);


            if (SessionState == SessionState.Busy && value == SessionState.Ready)
            {
                base.SessionState = SearchingPeriodForOneUser == TimeSpan.Zero ?
                    SessionState.Ready :
                    SessionState.Pending;
                PendingUntil = DateTime.Now + SearchingPeriodForOneUser;
                return;
            }

            base.SessionState = value;
        }
    }

    public TimeSpan SearchingPeriod { get; private set; }
    public DateTime PendingUntil { get; private set; }

    public TimeSpan SearchingPeriodForOneUser
        => SearchingPeriod / NumberOfUsers;
}