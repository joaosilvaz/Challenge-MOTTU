using Challenge_MOTTU.DTOs.Requests;
using Challenge_MOTTU.DTOs.Responses;
using Challenge_MOTTU.Exceptions;
using Challenge_MOTTU.Mappers;
using Challenge_MOTTU.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Challenge_MOTTU.Controllers
{
    [ApiController]
    [Route("pendings")]
    public class PendingController : ControllerBase
    {
        private readonly IPendingService _pendingService;
        private readonly LinkGenerator _linkGenerator;

        public PendingController(IPendingService context, LinkGenerator linkGenerator)
        {
            _pendingService = context;
            _linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Lista todas as pendências (alugueis de motos) com paginação.
        /// </summary>
        /// <param name="pageNumber">Número da página (padrão = 1)</param>
        /// <param name="pageSize">Quantidade de registros por página (padrão = 10)</param>
        /// <returns>Uma lista paginada de pendências com links HATEOAS.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<PendingResponse>), 200)]
        public async Task<ActionResult<PagedResponse<PendingResponse>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pendings = await _pendingService.GetAllAsync();

            var totalCount = pendings.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = pendings
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => p.ToResponse(_linkGenerator))
                .ToList();

            var response = new PagedResponse<PendingResponse>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Links = new Dictionary<string, string>
                {
                    { "self", _linkGenerator.GetPathByAction("GetAll", "Pending", new { pageNumber, pageSize }) ?? string.Empty },
                    { "next", pageNumber < totalPages ? _linkGenerator.GetPathByAction("GetAll", "Pending", new { pageNumber = pageNumber + 1, pageSize }) ?? string.Empty : string.Empty },
                    { "prev", pageNumber > 1 ? _linkGenerator.GetPathByAction("GetAll", "Pending", new { pageNumber = pageNumber - 1, pageSize }) ?? string.Empty : string.Empty }
                }
            };

            return Ok(response);
        }

        /// <summary>
        /// Busca uma pendência pelo ID.
        /// </summary>
        /// <param name="id">ID da pendência</param>
        /// <returns>A pendência encontrada ou 404 se não existir.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PendingResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PendingResponse>> GetById(int id)
        {
            try
            {
                var pending = await _pendingService.GetById(id);
                return Ok(pending.ToResponse(_linkGenerator));
            }
            catch (PendingNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Cria uma nova pendência (aluguel de uma bike por um usuário).
        /// </summary>
        /// <param name="request">Dados do aluguel</param>
        /// <returns>A pendência criada com links HATEOAS.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PendingResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PendingResponse>> Create(CreatePendingRequest request)
        {
            try
            {
                var pending = request.ToEntity();
                var pendingEntity = await _pendingService.CriarAsync(pending);

                return CreatedAtAction(nameof(GetById), new { id = pendingEntity.Id }, pendingEntity.ToResponse(_linkGenerator));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza a data final de uma pendência.
        /// </summary>
        /// <param name="id">ID da pendência</param>
        /// <param name="novaDataFim">Nova data final</param>
        /// <returns>204 se atualizado ou 404 se não encontrado.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] DateTime novaDataFim)
        {
            try
            {
                await _pendingService.AtualizarDataFimAsync(id, novaDataFim);
                return NoContent();
            }
            catch (PendingNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Exclui uma pendência do sistema.
        /// </summary>
        /// <param name="id">ID da pendência</param>
        /// <returns>204 se excluída ou 404 se não encontrada.</returns>
        [HttpPut("{id}/finalizar")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Finalizar(int id)
        {
            try
            {
                await _pendingService.FinalizarAsync(id);
                return NoContent();
            }
            catch (PendingNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui uma pendência do sistema.
        /// </summary>
        /// <param name="id">ID da pendência</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
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
        }
    }
}
