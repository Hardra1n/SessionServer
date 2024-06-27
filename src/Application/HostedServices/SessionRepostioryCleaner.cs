
using Contracts;

public class SessionRepositoryCleaner : IHostedService, IDisposable
{
    private readonly TimeSpan _waitingPeriod = TimeSpan.FromHours(1);
    private ILoggerManager _logger;
    private ISessionRepository _repository;
    private Timer? _timer;

    public SessionRepositoryCleaner(ILoggerManager logger, ISessionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInfo("Session repository cleaner starts working");
        _timer = new Timer(DeleteSessionIfNotChangedDuringPeriod, null, TimeSpan.Zero, _waitingPeriod);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInfo("Session repository cleaner stops working");

        Dispose();
        return Task.CompletedTask;
    }

    public void DeleteSessionIfNotChangedDuringPeriod(object? obj = null)
    {
        _logger.LogInfo("Starting cleaning session repository");
        var sessions = _repository.GetAllSessions();

        DateTime conditionalDate = DateTime.Now - _waitingPeriod;
        foreach (var session in sessions)
        {
            if (session.LastTimeModified < conditionalDate)
            {
                try
                {
                    _repository.DeleteSession(session);
                    _logger.LogInfo($"Session {session.Name} was cleaned, expiration time " +
                        $"{conditionalDate} lastmodified {session.LastTimeModified}");
                }
                catch { }
            }
        }
        _logger.LogInfo("Session repository is cleaned");
    }
}