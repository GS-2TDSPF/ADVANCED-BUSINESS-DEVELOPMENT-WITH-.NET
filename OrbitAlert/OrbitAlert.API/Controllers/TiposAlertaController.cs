using Microsoft.AspNetCore.Mvc;
using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Services;

namespace OrbitAlert.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TiposAlertaController(ITipoAlertaService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(TipoAlertaResponse), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] TipoAlertaRequest request)
    {
        var tipo = service.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = tipo.Id }, tipo);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TipoAlertaResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(service.GetAll());

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(TipoAlertaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(long id)
    {
        var tipo = service.GetById(id);
        if (tipo is null) return NotFound($"Tipo de alerta com id '{id}' não encontrado.");
        return Ok(tipo);
    }

    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(TipoAlertaResponse), StatusCodes.Status200OK)]
    public IActionResult Update(long id, [FromBody] TipoAlertaRequest request) => Ok(service.Update(id, request));

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(long id)
    {
        if (!service.Delete(id)) return NotFound($"Tipo de alerta com id '{id}' não encontrado.");
        return NoContent();
    }
}
