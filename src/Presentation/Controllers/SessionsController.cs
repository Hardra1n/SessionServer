using Contracts;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Presentation;

[ApiController]
[Route("/api/sessions")]
public class SessionsController : ControllerBase
{
    private readonly ILoggerManager _logger;
    private readonly ISessionService _service;

    public SessionsController(ILoggerManager logger, ISessionService sessionService)
    {
        _logger = logger;
        _service = sessionService;
    }
}