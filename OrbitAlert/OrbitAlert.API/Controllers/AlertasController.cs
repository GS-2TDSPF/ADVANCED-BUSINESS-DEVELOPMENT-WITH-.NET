using Microsoft.AspNetCore.Mvc;
using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Services;
using OrbitAlert.Domain.Enum;

namespace OrbitAlert.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlertasController(IAlertaService alertaService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(AlertaResponse), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] AlertaRequest request)
    {
        var alerta = alertaService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = alerta.Id }, alerta);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AlertaResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(alertaService.GetAll());

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(AlertaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(long id)
    {
        var alerta = alertaService.GetById(id);
        if (alerta is null) return NotFound($"Alerta com id '{id}' não encontrado.");
        return Ok(alerta);
    }

    [HttpGet("status")]
    [ProducesResponseType(typeof(IEnumerable<AlertaResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByStatus([FromQuery] StatusAlertaEnum status) => Ok(alertaService.GetByStatus(status));

    [HttpGet("municipio/{idMunicipio:long}")]
    [ProducesResponseType(typeof(IEnumerable<AlertaResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByMunicipio(long idMunicipio) => Ok(alertaService.GetByMunicipio(idMunicipio));

    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(AlertaResponse), StatusCodes.Status200OK)]
    public IActionResult Update(long id, [FromBody] AlertaRequest request) => Ok(alertaService.Update(id, request));

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(long id)
    {
        if (!alertaService.Delete(id)) return NotFound($"Alerta com id '{id}' não encontrado.");
        return NoContent();
    }
}
