using Challenge_MOTTU.DTOs.Requests;
using Challenge_MOTTU.DTOs.Responses;
using Challenge_MOTTU.Exceptions;
using Challenge_MOTTU.Mappers;
using Challenge_MOTTU.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Challenge_MOTTU.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly LinkGenerator _linkGenerator;

        public UsuarioController(IUsuarioService context, LinkGenerator linkGenerator)
        {
            _usuarioService = context;
            _linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Retorna todos os usuários com paginação.
        /// </summary>
        /// <param name="pageNumber">Número da página (padrão = 1)</param>
        /// <param name="pageSize">Quantidade de itens por página (padrão = 10)</param>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<UsuarioResponse>), 200)]
        public async Task<ActionResult<PagedResponse<UsuarioResponse>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var usuarios = await _usuarioService.GetAllAsync();

            var totalCount = usuarios.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = usuarios
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(u => u.ToResponse(_linkGenerator)) 
                .ToList();

            var response = new PagedResponse<UsuarioResponse>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Links = new Dictionary<string, string?>
                {
                    { "self", _linkGenerator.GetPathByAction("GetAll", "Usuario", new { pageNumber, pageSize }) },
                    { "next", pageNumber < totalPages ? _linkGenerator.GetPathByAction("GetAll", "Usuario", new { pageNumber = pageNumber + 1, pageSize }) : null },
                    { "prev", pageNumber > 1 ? _linkGenerator.GetPathByAction("GetAll", "Usuario", new { pageNumber = pageNumber - 1, pageSize }) : null }
                }
            };

            return Ok(response);
        }

        /// <summary>
        /// Busca usuário pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsuarioResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UsuarioResponse>> GetById(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetById(id);
                return Ok(usuario.ToResponse(_linkGenerator));
            }
            catch (UsuarioNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(UsuarioResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UsuarioResponse>> Create(CreateUsuarioRequest request)
        {
            var usuario = request.ToEntity();
            var usuarioEntity = await _usuarioService.CriarAsync(usuario);

            return CreatedAtAction(nameof(GetById), new { id = usuarioEntity.Id }, usuarioEntity.ToResponse(_linkGenerator));
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, UpdateUsuarioRequest request)
        {
            var usuarioAtualizado = request.ToEntity(id);
            var usuario = await _usuarioService.AtualizarAsync(id, usuarioAtualizado);

            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            return NoContent();
        }

        /// <summary>
        /// Remove um usuário.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            await _usuarioService.DeletarAsync(id);
            return NoContent();
        }
    }
}
