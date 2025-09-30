using Challenge_MOTTU.DTOs.Requests;
using Challenge_MOTTU.DTOs.Responses;
using Challenge_MOTTU.Model;

namespace Challenge_MOTTU.Mappers
{
    public static class BikeMapper
    {
        public static Bike ToEntity(this CreateBikeRequest request)
        {
            return new Bike
            {
                Modelo = request.Modelo,
                Placa = request.Placa,
                Ano = request.Ano,
                Disponivel = true
            };
        }

        public static BikeResponse ToResponse(this Bike bike)
        {
            return new BikeResponse
            {
                Id = bike.Id,
                Modelo = bike.Modelo,
                Placa = bike.Placa,
                Ano = bike.Ano,
                Disponivel = bike.Disponivel
            };
        }
    }
}
