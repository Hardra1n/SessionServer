using Entities.Sessions;

public static class RepositoryExtensions
{
    public static void PopulateSessionRepository(this WebApplication app)
    {
        var repository = app.Services.GetService<ISessionRepository>();
        if (repository == null)
            return;
        repository.CreateSession(new Session("Hello"));
    }
}