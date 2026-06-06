using Microsoft.AspNetCore.Mvc;
using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Services;

namespace OrbitAlert.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificacoesController(INotificacaoService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(NotificacaoResponse), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] NotificacaoRequest request)
    {
        var notificacao = service.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = notificacao.Id }, notificacao);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<NotificacaoResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(service.GetAll());

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(NotificacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(long id)
    {
        var notificacao = service.GetById(id);
        if (notificacao is null) return NotFound($"Notificação com id '{id}' não encontrada.");
        return Ok(notificacao);
    }

    [HttpGet("usuario/{idUsuario:long}")]
    [ProducesResponseType(typeof(IEnumerable<NotificacaoResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByUsuario(long idUsuario) => Ok(service.GetByUsuario(idUsuario));

    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(NotificacaoResponse), StatusCodes.Status200OK)]
    public IActionResult Update(long id, [FromBody] NotificacaoRequest request) => Ok(service.Update(id, request));

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(long id)
    {
        if (!service.Delete(id)) return NotFound($"Notificação com id '{id}' não encontrada.");
        return NoContent();
    }
}
