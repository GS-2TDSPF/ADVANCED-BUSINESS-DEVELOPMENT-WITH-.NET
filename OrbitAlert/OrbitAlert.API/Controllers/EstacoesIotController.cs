using Microsoft.AspNetCore.Mvc;
using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Services;

namespace OrbitAlert.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EstacoesIotController(IEstacaoIotService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(EstacaoIotResponse), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] EstacaoIotRequest request)
    {
        var estacao = service.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = estacao.Id }, estacao);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EstacaoIotResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(service.GetAll());

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(EstacaoIotResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(long id)
    {
        var estacao = service.GetById(id);
        if (estacao is null) return NotFound($"Estação IoT com id '{id}' não encontrada.");
        return Ok(estacao);
    }

    [HttpGet("zona/{idZona:long}")]
    [ProducesResponseType(typeof(IEnumerable<EstacaoIotResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByZona(long idZona) => Ok(service.GetByZona(idZona));

    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(EstacaoIotResponse), StatusCodes.Status200OK)]
    public IActionResult Update(long id, [FromBody] EstacaoIotRequest request) => Ok(service.Update(id, request));

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(long id)
    {
        if (!service.Delete(id)) return NotFound($"Estação IoT com id '{id}' não encontrada.");
        return NoContent();
    }
}
