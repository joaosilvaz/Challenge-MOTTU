using Challenge_MOTTU.Exceptions;
using Challenge_MOTTU.Interfaces;
using Challenge_MOTTU.Model;
using Microsoft.AspNetCore.Mvc;

namespace Challenge_MOTTU.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService context)
        {
            _usuarioService = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllAsync();
                return Ok(usuarios);
            }
            catch (UsuarioException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetById(id);

                return Ok(usuario);
            }
            catch (UsuarioException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<Usuario>> GetByEmail([FromQuery] string email)
        {
            try
            {
                var usuario = await _usuarioService.GetByEmail(email);
                return Ok(usuario);

            }
            catch (UsuarioException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Create(Usuario usuario)
        {
            try
            {
                Usuario usuarioEntity = await _usuarioService.CriarAsync(usuario);

                return CreatedAtAction(nameof(GetById), new { id = usuarioEntity.Id }, usuarioEntity);
            }
            catch (UsuarioException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Usuario usuarioAtualizado)
        {
            try
            {
                if (id != usuarioAtualizado.Id)
                    return BadRequest("ID do usuário inválido.");

                await _usuarioService.AtualizarAsync(id, usuarioAtualizado);
                return NoContent();
            }
            catch (UsuarioException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _usuarioService.DeletarAsync(id);
                return NoContent();
            }
            catch (UsuarioException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }
    }
}
