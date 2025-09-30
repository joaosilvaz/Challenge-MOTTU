using Challenge_MOTTU.DTOs.Requests;
using Challenge_MOTTU.DTOs.Responses;
using Challenge_MOTTU.Model;
using Microsoft.AspNetCore.Routing;

namespace Challenge_MOTTU.Mappers
{
    public static class PendingMapper
    {
        public static Pending ToEntity(this CreatePendingRequest request)
        {
            return new Pending
            {
                UsuarioId = request.UsuarioId,
                BikeId = request.BikeId,
                DataInicio = request.DataInicio,
                DataFim = request.DataFim,
                Status = Enums.StatusAluguel.Pendente
            };
        }

        public static PendingResponse ToResponse(this Pending pending, LinkGenerator linkGenerator)
        {
            return new PendingResponse
            {
                Id = pending.Id,
                Status = pending.Status.ToString(),
                DataInicio = pending.DataInicio,
                DataFim = pending.DataFim,
                UsuarioId = pending.UsuarioId,
                Usuario = pending.Usuario?.ToResponse(linkGenerator), 
                BikeId = pending.BikeId,
                Bike = pending.Bike?.ToResponse(), 
                Links = new Dictionary<string, string>
                {
                    { "self", linkGenerator.GetPathByAction("GetById", "Pending", new { id = pending.Id }) ?? string.Empty },
                    { "finalizar", linkGenerator.GetPathByAction("Finalizar", "Pending", new { id = pending.Id }) ?? string.Empty },
                    { "delete", linkGenerator.GetPathByAction("Delete", "Pending", new { id = pending.Id }) ?? string.Empty }
                }
            };
        }
    }
}
