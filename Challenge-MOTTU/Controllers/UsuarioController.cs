using Challenge_MOTTU.DTOs.Requests;
using Challenge_MOTTU.DTOs.Responses;
using Challenge_MOTTU.Exceptions;
using Challenge_MOTTU.Mappers;
using Challenge_MOTTU.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/usuarios")]
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
        /// Lista todos os usuários cadastrados com paginação.
        /// </summary>
        /// <param name="pageNumber">Número da página (padrão = 1)</param>
        /// <param name="pageSize">Quantidade de registros por página (padrão = 10)</param>
        /// <returns>Uma lista paginada de usuários com links HATEOAS.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<UsuarioResponse>), 200)]
        public async Task<ActionResult<PagedResponse<UsuarioResponse>>> GetAll(
          [FromQuery] int pageNumber = 1,
          [FromQuery] int pageSize = 10)
        {
            var usuarios = await _usuarioService.GetAllAsync();

            var totalCount = usuarios.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // Captura automaticamente a versão atual da API
            var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "2.0";

            var items = usuarios
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(u =>
                {
                    var response = u.ToResponse(_linkGenerator);
                    response.Links = new Dictionary<string, string?>
                    {
                { "self", _linkGenerator.GetPathByAction("GetById", "Usuario", new { version, id = u.Id }) },
                { "update", _linkGenerator.GetPathByAction("Update", "Usuario", new { version, id = u.Id }) },
                { "delete", _linkGenerator.GetPathByAction("Delete", "Usuario", new { version, id = u.Id }) }
                    };
                    return response;
                })
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
            { "self", _linkGenerator.GetPathByAction("GetAll", "Usuario", new { version, pageNumber, pageSize }) },
            { "next", pageNumber < totalPages ? _linkGenerator.GetPathByAction("GetAll", "Usuario", new { version, pageNumber = pageNumber + 1, pageSize }) : null },
            { "prev", pageNumber > 1 ? _linkGenerator.GetPathByAction("GetAll", "Usuario", new { version, pageNumber = pageNumber - 1, pageSize }) : null }
        }
            };

            return Ok(response);
        }


        /// <summary>
        /// Busca um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>O usuário encontrado ou 404 se não existir.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsuarioResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UsuarioResponse>> GetById(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetById(id);
                var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "2.0";

                var response = usuario.ToResponse(_linkGenerator);
                response.Links = new Dictionary<string, string?>
        {
            { "self", _linkGenerator.GetPathByAction("GetById", "Usuario", new { version, id }) },
            { "update", _linkGenerator.GetPathByAction("Update", "Usuario", new { version, id }) },
            { "delete", _linkGenerator.GetPathByAction("Delete", "Usuario", new { version, id }) }
        };

                return Ok(response);
            }
            catch (UsuarioNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="request">Dados para criação do usuário</param>
        /// <returns>O usuário criado com links HATEOAS.</returns>
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
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <param name="request">Novos dados do usuário</param>
        /// <returns>204 se atualizado com sucesso ou 404 se não encontrado.</returns>
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
        /// Exclui um usuário do sistema.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>204 se excluído com sucesso ou 404 se não encontrado.</returns>
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
