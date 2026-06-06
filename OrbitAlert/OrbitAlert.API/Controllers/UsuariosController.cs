using Microsoft.AspNetCore.Mvc;
using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Services;

namespace OrbitAlert.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController(IUsuarioService usuarioService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] UsuarioRequest request)
    {
        var usuario = usuarioService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UsuarioResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(usuarioService.GetAll());

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(long id)
    {
        var usuario = usuarioService.GetById(id);
        if (usuario is null) return NotFound($"Usuário com id '{id}' não encontrado.");
        return Ok(usuario);
    }

    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(long id, [FromBody] UsuarioRequest request)
        => Ok(usuarioService.Update(id, request));

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(long id)
    {
        if (!usuarioService.Delete(id)) return NotFound($"Usuário com id '{id}' não encontrado.");
        return NoContent();
    }
}
