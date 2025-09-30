using Challenge_MOTTU.DTOs.Requests;
using Challenge_MOTTU.DTOs.Responses;
using Challenge_MOTTU.Model;
using Microsoft.AspNetCore.Routing;

namespace Challenge_MOTTU.Mappers
{
    public static class UsuarioMapper
    {
        public static Usuario ToEntity(this CreateUsuarioRequest request)
        {
            return new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = request.Senha
            };
        }

        public static Usuario ToEntity(this UpdateUsuarioRequest request, int id)
        {
            return new Usuario
            {
                Id = id,
                Nome = request.Nome,
                Email = request.Email,
                Senha = request.Senha
            };
        }

        public static UsuarioResponse ToResponse(this Usuario usuario, LinkGenerator linkGenerator)
        {
            return new UsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Links = new Dictionary<string, string>
                {
                    { "self", linkGenerator.GetPathByAction("GetById", "Usuario", new { id = usuario.Id }) ?? string.Empty },
                    { "update", linkGenerator.GetPathByAction("Update", "Usuario", new { id = usuario.Id }) ?? string.Empty },
                    { "delete", linkGenerator.GetPathByAction("Delete", "Usuario", new { id = usuario.Id }) ?? string.Empty }
                }
            };
        }
    }
}
