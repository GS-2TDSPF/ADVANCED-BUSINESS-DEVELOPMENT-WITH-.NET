using Microsoft.AspNetCore.Mvc;
using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Services;

namespace OrbitAlert.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ZonasRiscoController(IZonaRiscoService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ZonaRiscoResponse), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] ZonaRiscoRequest request)
    {
        var zona = service.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = zona.Id }, zona);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ZonaRiscoResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(service.GetAll());

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(ZonaRiscoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(long id)
    {
        var zona = service.GetById(id);
        if (zona is null) return NotFound($"Zona de risco com id '{id}' não encontrada.");
        return Ok(zona);
    }

    [HttpGet("municipio/{idMunicipio:long}")]
    [ProducesResponseType(typeof(IEnumerable<ZonaRiscoResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByMunicipio(long idMunicipio) => Ok(service.GetByMunicipio(idMunicipio));

    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(ZonaRiscoResponse), StatusCodes.Status200OK)]
    public IActionResult Update(long id, [FromBody] ZonaRiscoRequest request) => Ok(service.Update(id, request));

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(long id)
    {
        if (!service.Delete(id)) return NotFound($"Zona de risco com id '{id}' não encontrada.");
        return NoContent();
    }
}
