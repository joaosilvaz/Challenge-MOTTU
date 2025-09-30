using Challenge_MOTTU.Connection;
using Challenge_MOTTU.Exceptions;
using Challenge_MOTTU.Model;
using Challenge_MOTTU.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Challenge_MOTTU.Services
{
    public class BikeService : IBikeService
    {
        private readonly AppDbContext _context;

        public BikeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Bike> CriarAsync(Bike bike)
        {
            if (string.IsNullOrWhiteSpace(bike.Modelo))
                throw new BikeNotFoundException("O campo Modelo é obrigatório.");

            if (string.IsNullOrWhiteSpace(bike.Placa))
                throw new BikeNotFoundException("O campo Placa é obrigatório.");

            var placaExistente = await _context.Bikes
                .FirstOrDefaultAsync(b => b.Placa.Trim().ToUpper() == bike.Placa.Trim().ToUpper());

            if (placaExistente != null)
                throw new BikeNotFoundException("Já existe uma moto cadastrada com essa placa.");

            if (bike.Ano <= 0)
                throw new BikeNotFoundException("O campo Ano é obrigatório.");

            bike.Disponivel = true;

            _context.Bikes.Add(bike);
            await _context.SaveChangesAsync();

            return bike;
        }


        public async Task<IEnumerable<Bike>> GetAllAsync()
        {
            var bikes = await _context.Bikes.ToListAsync();

            if (bikes == null || !bikes.Any())
                throw new BikeNotFoundException("Nenhuma bike encontrada.");

            return bikes;
        }

        public async Task<Bike> GetById(int id)
        {
            var bike = await _context.Bikes.FirstOrDefaultAsync(b => b.Id == id);

            if (bike == null)
                throw new BikeNotFoundException();

            return bike;
        }

        public async Task AtualizarDisponibilidadeAsync(int id, bool disponivel)
        {
            var bike = await _context.Bikes.FirstOrDefaultAsync(b => b.Id == id);
            if (bike == null)
                throw new BikeNotFoundException("Bike não encontrada.");

            bike.Disponivel = disponivel;

            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(int id)
        {
            var bike = await _context.Bikes.FirstOrDefaultAsync(b => b.Id == id);

            if (bike == null)
                throw new BikeNotFoundException();

            _context.Bikes.Remove(bike);
            await _context.SaveChangesAsync();
        }
    }
}
