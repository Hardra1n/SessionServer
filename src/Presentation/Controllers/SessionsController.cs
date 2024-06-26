using Contracts;
using Entities.Sessions;
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

    [HttpGet("{sessionName}", Name = "SessionByName")]
    public IActionResult GetSessionByName(string sessionName)
    {
        var session = _service.GetSession(sessionName);

        return Ok(session);
    }

    [HttpPost]
    public IActionResult CreateSession([FromBody] SessionForCreationDto session)
    {
        var createdSession = _service.CreateSession(session);

        return CreatedAtRoute("SessionByName", new { sessionName = session.Name }, createdSession);
    }

    [HttpPut("{sessionName}")]
    public IActionResult UpdateSession(string sessionName, [FromBody] SessionForUpdateDto session)
    {
        _service.UpdateSession(sessionName, session);

        return NoContent();
    }
}