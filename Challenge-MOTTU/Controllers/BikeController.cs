using Challenge_MOTTU.Exceptions;
using Challenge_MOTTU.Model;
using Challenge_MOTTU.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Challenge_MOTTU.Controllers
{
    [ApiController]
    [Route("bikes")]
    public class BikeController : ControllerBase
    {
        private readonly IBikeService _bikeService;

        public BikeController(IBikeService context)
        {
            _bikeService = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bike>>> GetAll()
        {
            try
            {
                var bikes = await _bikeService.GetAllAsync();
                return Ok(bikes);
            }
            catch (BikeNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bike>> GetById(int id)
        {
            try
            {
                var bike = await _bikeService.GetById(id);
                return Ok(bike);
            }
            catch (BikeNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Bike>> Create(Bike bike)
        {
            try
            {
                var bikeEntity = await _bikeService.CriarAsync(bike);
                return CreatedAtAction(nameof(GetById), new { id = bikeEntity.Id }, bikeEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Bike bikeAtualizada)
        {
            try
            {
                if (id != bikeAtualizada.Id)
                    return BadRequest("ID da bike inválido.");

                await _bikeService.AtualizarAsync(id, bikeAtualizada);
                return NoContent();
            }
            catch (BikeNotFoundException ex)
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
                await _bikeService.DeletarAsync(id);
                return NoContent();
            }
            catch (BikeNotFoundException ex)
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
