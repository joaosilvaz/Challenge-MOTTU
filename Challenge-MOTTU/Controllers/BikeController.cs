using Challenge_MOTTU.DTOs.Requests;
using Challenge_MOTTU.DTOs.Responses;
using Challenge_MOTTU.Exceptions;
using Challenge_MOTTU.Mappers;
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
        public async Task<ActionResult<IEnumerable<BikeResponse>>> GetAll()
        {
            var bikes = await _bikeService.GetAllAsync();
            return Ok(bikes.Select(b => b.ToResponse()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BikeResponse>> GetById(int id)
        {
            try
            {
                var bike = await _bikeService.GetById(id);
                return Ok(bike.ToResponse());
            }
            catch (BikeNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<BikeResponse>> Create(CreateBikeRequest request)
        {
            var bike = request.ToEntity();
            var bikeEntity = await _bikeService.CriarAsync(bike);

            return CreatedAtAction(nameof(GetById), new { id = bikeEntity.Id }, bikeEntity.ToResponse());
        }

        [HttpPut("{id}/disponibilidade")]
        public async Task<IActionResult> AtualizarDisponibilidade(int id, [FromBody] bool disponivel)
        {
            try
            {
                await _bikeService.AtualizarDisponibilidadeAsync(id, disponivel);
                return NoContent();
            }
            catch (BikeNotFoundException ex)
            {
                return NotFound(ex.Message);
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
        }
    }
}
