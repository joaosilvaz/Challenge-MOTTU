using Challenge_MOTTU.DTOs.Requests;
using Challenge_MOTTU.DTOs.Responses;
using Challenge_MOTTU.Exceptions;
using Challenge_MOTTU.Mappers;
using Challenge_MOTTU.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Challenge_MOTTU.Controllers
{
    [ApiController]
    [Route("bikes")]
    public class BikeController : ControllerBase
    {
        private readonly IBikeService _bikeService;
        private readonly LinkGenerator _linkGenerator;

        public BikeController(IBikeService context, LinkGenerator linkGenerator)
        {
            _bikeService = context;
            _linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Lista todas as motos cadastradas com paginação.
        /// </summary>
        /// <param name="pageNumber">Número da página (padrão = 1)</param>
        /// <param name="pageSize">Quantidade de registros por página (padrão = 10)</param>
        /// <returns>Uma lista paginada de motos com links HATEOAS.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<BikeResponse>), 200)]
        public async Task<ActionResult<PagedResponse<BikeResponse>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var bikes = await _bikeService.GetAllAsync();

            var totalCount = bikes.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = bikes
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(b =>
                {
                    var response = b.ToResponse();
                    response.Links = new Dictionary<string, string>
                    {
                        { "self", _linkGenerator.GetPathByAction("GetById", "Bike", new { id = b.Id }) ?? string.Empty },
                        { "updateDisponibilidade", _linkGenerator.GetPathByAction("AtualizarDisponibilidade", "Bike", new { id = b.Id }) ?? string.Empty },
                        { "delete", _linkGenerator.GetPathByAction("Delete", "Bike", new { id = b.Id }) ?? string.Empty }
                    };
                    return response;
                })
                .ToList();

            var responsePaged = new PagedResponse<BikeResponse>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Links = new Dictionary<string, string>
                {
                    { "self", _linkGenerator.GetPathByAction("GetAll", "Bike", new { pageNumber, pageSize }) ?? string.Empty },
                    { "next", pageNumber < totalPages ? _linkGenerator.GetPathByAction("GetAll", "Bike", new { pageNumber = pageNumber + 1, pageSize }) ?? string.Empty : null },
                    { "prev", pageNumber > 1 ? _linkGenerator.GetPathByAction("GetAll", "Bike", new { pageNumber = pageNumber - 1, pageSize }) ?? string.Empty : null }
                }
            };

            return Ok(responsePaged);
        }

        /// <summary>
        /// Busca uma moto pelo ID.
        /// </summary>
        /// <param name="id">ID da moto</param>
        /// <returns>A moto encontrada ou 404 se não existir.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BikeResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BikeResponse>> GetById(int id)
        {
            try
            {
                var bike = await _bikeService.GetById(id);
                var response = bike.ToResponse();

                // HATEOAS
                response.Links = new Dictionary<string, string>
                {
                    { "self", _linkGenerator.GetPathByAction("GetById", "Bike", new { id }) ?? string.Empty },
                    { "updateDisponibilidade", _linkGenerator.GetPathByAction("AtualizarDisponibilidade", "Bike", new { id }) ?? string.Empty },
                    { "delete", _linkGenerator.GetPathByAction("Delete", "Bike", new { id }) ?? string.Empty }
                };

                return Ok(response);
            }
            catch (BikeNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Cadastra uma nova moto.
        /// </summary>
        /// <param name="request">Dados da moto</param>
        /// <returns>A moto criada com links HATEOAS.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BikeResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BikeResponse>> Create(CreateBikeRequest request)
        {
            var bike = request.ToEntity();
            var bikeEntity = await _bikeService.CriarAsync(bike);

            var response = bikeEntity.ToResponse();
            response.Links = new Dictionary<string, string>
            {
                { "self", _linkGenerator.GetPathByAction("GetById", "Bike", new { id = bikeEntity.Id }) ?? string.Empty },
                { "updateDisponibilidade", _linkGenerator.GetPathByAction("AtualizarDisponibilidade", "Bike", new { id = bikeEntity.Id }) ?? string.Empty },
                { "delete", _linkGenerator.GetPathByAction("Delete", "Bike", new { id = bikeEntity.Id }) ?? string.Empty }
            };

            return CreatedAtAction(nameof(GetById), new { id = bikeEntity.Id }, response);
        }

        /// <summary>
        /// Atualiza a disponibilidade de uma moto (true = disponível, false = indisponível).
        /// </summary>
        /// <param name="id">ID da moto</param>
        /// <param name="disponivel">Novo estado de disponibilidade</param>
        /// <returns>204 se atualizado ou 404 se não encontrado.</returns>
        [HttpPut("{id}/disponibilidade")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
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

        /// <summary>
        /// Exclui uma moto do sistema.
        /// </summary>
        /// <param name="id">ID da moto</param>
        /// <returns>204 se excluída ou 404 se não encontrada.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
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
