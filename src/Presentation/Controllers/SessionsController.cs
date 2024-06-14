using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Presentation;

[ApiController]
[Route("/api/sessions")]
public class SessionsController : ControllerBase
{
    private readonly ILoggerManager _logger;

    public SessionsController(ILoggerManager logger)
    {
        _logger = logger;
    }
}