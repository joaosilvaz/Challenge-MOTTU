using Challenge_MOTTU.Connection;
using Challenge_MOTTU.Exceptions;
using Challenge_MOTTU.Interfaces;
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
            // Validações básicas
            if (pending.UsuarioId <= 0)
                throw new PendingNotFoundException("Usuário inválido.");

            if (pending.BikeId <= 0)
                throw new PendingNotFoundException("Bike inválida.");

            if (pending.DataInicio == default)
                throw new PendingNotFoundException("A Data de Início é obrigatória.");

            _context.Pendings.Add(pending);
            await _context.SaveChangesAsync();

            return pending;
        }

        public async Task<IEnumerable<Pending>> GetAllAsync()
        {
            var pendings = await _context.Pendings
                                         .Include(p => p.Usuario)
                                         .Include(p => p.Bike)
                                         .ToListAsync();

            if (pendings == null || !pendings.Any())
                throw new PendingNotFoundException("Nenhuma pendência encontrada.");

            return pendings;
        }

        public async Task<Pending> GetById(int id)
        {
            var pending = await _context.Pendings
                                        .Include(p => p.Usuario)
                                        .Include(p => p.Bike)
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if (pending is null)
                throw new PendingNotFoundException();

            return pending;
        }

        public async Task<Pending> AtualizarAsync(int id, Pending pendingAtualizada)
        {
            var pending = await _context.Pendings.FirstOrDefaultAsync(p => p.Id == id);
            if (pending == null)
                throw new PendingNotFoundException("Pendência não encontrada.");

            // Atualiza apenas os campos principais
            pending.Status = pendingAtualizada.Status;
            pending.DataInicio = pendingAtualizada.DataInicio;
            pending.DataFim = pendingAtualizada.DataFim;
            pending.UsuarioId = pendingAtualizada.UsuarioId;
            pending.BikeId = pendingAtualizada.BikeId;

            await _context.SaveChangesAsync();
            return pending;
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
