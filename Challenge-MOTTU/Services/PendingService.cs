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

            var aluguelAtivo = await _context.Pendings
                .FirstOrDefaultAsync(p => p.UsuarioId == pending.UsuarioId &&
                                          (p.Status == Enums.StatusAluguel.Pendente ||
                                           p.Status == Enums.StatusAluguel.Aprovado));

            if (aluguelAtivo != null)
                throw new PendingNotFoundException("Usuário já possui um aluguel ativo.");

            var bike = await _context.Bikes.FirstOrDefaultAsync(b => b.Id == pending.BikeId);
            if (bike == null)
                throw new PendingNotFoundException("Bike não encontrada.");

            if (!bike.Disponivel)
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

        public async Task AtualizarDataFimAsync(int id, DateTime novaDataFim)
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
            var pending = await _context.Pendings
                                        .Include(p => p.Bike)
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if (pending == null)
                throw new PendingNotFoundException("Pendência não encontrada.");

            if (pending.Status == Enums.StatusAluguel.Concluido)
                throw new PendingNotFoundException("Essa pendência já foi concluída.");

            pending.Status = Enums.StatusAluguel.Concluido;
            pending.DataFim = DateTime.UtcNow;

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
