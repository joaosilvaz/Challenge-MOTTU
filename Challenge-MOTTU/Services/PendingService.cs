using Challenge_MOTTU.Connection;
using Challenge_MOTTU.Exceptions;
using Challenge_MOTTU.Model;
using Challenge_MOTTU.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Challenge_MOTTU.Services
{
    public class PendingService : IPendingService
    {
        private readonly AppDbContext _context;

        public PendingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Pending> CriarAsync(Pending pending)
        {
            if (pending.UsuarioId <= 0)
                throw new PendingNotFoundException("Usuário inválido.");

            if (pending.BikeId <= 0)
                throw new PendingNotFoundException("Bike inválida.");

            if (pending.DataInicio == default)
                throw new PendingNotFoundException("A Data de Início é obrigatória.");

            if (pending.DataFim == default)
                throw new PendingNotFoundException("A Data de Fim é obrigatória.");

            if (pending.DataFim <= pending.DataInicio)
                throw new PendingNotFoundException("A Data de Fim deve ser maior que a Data de Início.");

            var bike = await _context.Bikes.FindAsync(pending.BikeId);
            if (bike == null || !bike.Disponivel)
                throw new PendingNotFoundException("Bike não está disponível.");

            bike.Disponivel = false;
            pending.Status = Enums.StatusAluguel.Pendente;

            _context.Pendings.Add(pending);
            await _context.SaveChangesAsync();

            return pending;
        }

        public async Task<IEnumerable<Pending>> GetAllAsync()
        {
            return await _context.Pendings
                                 .Include(p => p.Usuario)
                                 .Include(p => p.Bike)
                                 .ToListAsync();
        }

        public async Task<Pending> GetById(int id)
        {
            var pending = await _context.Pendings
                                        .Include(p => p.Usuario)
                                        .Include(p => p.Bike)
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if (pending is null)
                throw new PendingNotFoundException("Pendência não encontrada.");

            return pending;
        }

        public async Task AtualizarDataFimAsync(int id, DateTimeOffset novaDataFim)
        {
            var pending = await _context.Pendings.FirstOrDefaultAsync(p => p.Id == id);
            if (pending == null)
                throw new PendingNotFoundException("Pendência não encontrada.");

            if (novaDataFim <= pending.DataInicio)
                throw new PendingNotFoundException("Data de fim deve ser maior que a data de início.");

            pending.DataFim = novaDataFim;

            await _context.SaveChangesAsync();
        }

        public async Task FinalizarAsync(int id)
        {
            var pending = await _context.Pendings.Include(p => p.Bike)
                                                 .FirstOrDefaultAsync(p => p.Id == id);

            if (pending == null)
                throw new PendingNotFoundException("Pendência não encontrada.");

            if (pending.Status == Enums.StatusAluguel.Concluido)
                throw new PendingNotFoundException("Essa pendência já foi concluída.");

            if (DateTimeOffset.UtcNow < pending.DataFim)
                throw new PendingNotFoundException(
                    $"Não é possível finalizar antes da data prevista ({pending.DataFim}).");

            pending.Status = Enums.StatusAluguel.Concluido;
            pending.DataFim = DateTimeOffset.UtcNow;

            if (pending.Bike != null)
                pending.Bike.Disponivel = true;

            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(int id)
        {
            var pending = await _context.Pendings.FirstOrDefaultAsync(p => p.Id == id);
            if (pending == null)
                throw new PendingNotFoundException("Pendência não encontrada.");

            _context.Pendings.Remove(pending);
            await _context.SaveChangesAsync();
        }
    }
}
