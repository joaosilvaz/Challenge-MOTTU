using Challenge_MOTTU.Exceptions;
using Challenge_MOTTU.Interfaces;
using Challenge_MOTTU.Model;
using Challenge_MOTTU.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Challenge_MOTTU.Controllers
{
    [ApiController]
    [Route("pendings")]
    public class PendingController : ControllerBase
    {
        private readonly IPendingService _pendingService;

        public PendingController(IPendingService context)
        {
            _pendingService = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pending>>> GetAll()
        {
            try
            {
                var pendings = await _pendingService.GetAllAsync();
                return Ok(pendings);
            }
            catch (PendingNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pending>> GetById(int id)
        {
            try
            {
                var pending = await _pendingService.GetById(id);
                return Ok(pending);
            }
            catch (PendingNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Pending>> Create(Pending pending)
        {
            try
            {
                var pendingEntity = await _pendingService.CriarAsync(pending);
                return CreatedAtAction(nameof(GetById), new { id = pendingEntity.Id }, pendingEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Pending pendingAtualizada)
        {
            try
            {
                if (id != pendingAtualizada.Id)
                    return BadRequest("ID da pendência inválido.");

                await _pendingService.AtualizarAsync(id, pendingAtualizada);
                return NoContent();
            }
            catch (PendingNotFoundException ex)
            {
                return NotFound(ex.Message);
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
                await _pendingService.DeletarAsync(id);
                return NoContent();
            }
            catch (PendingNotFoundException ex)
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
