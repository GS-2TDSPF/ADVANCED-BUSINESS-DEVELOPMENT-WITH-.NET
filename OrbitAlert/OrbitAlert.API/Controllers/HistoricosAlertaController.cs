using Microsoft.AspNetCore.Mvc;
using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Services;

namespace OrbitAlert.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HistoricosAlertaController(IHistoricoAlertaService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(HistoricoAlertaResponse), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] HistoricoAlertaRequest request)
    {
        var historico = service.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = historico.Id }, historico);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<HistoricoAlertaResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(service.GetAll());

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(HistoricoAlertaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(long id)
    {
        var historico = service.GetById(id);
        if (historico is null) return NotFound($"Histórico com id '{id}' não encontrado.");
        return Ok(historico);
    }

    [HttpGet("alerta/{idAlerta:long}")]
    [ProducesResponseType(typeof(IEnumerable<HistoricoAlertaResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByAlerta(long idAlerta) => Ok(service.GetByAlerta(idAlerta));

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(long id)
    {
        if (!service.Delete(id)) return NotFound($"Histórico com id '{id}' não encontrado.");
        return NoContent();
    }
}
