using Challenge_MOTTU.Model;

namespace Challenge_MOTTU.Services.Abstractions
{
    public interface IPendingService
    {
        Task<Pending> CriarAsync(Pending pending);
        Task<IEnumerable<Pending>> GetAllAsync();
        Task<Pending> GetById(int id);
        Task DeletarAsync(int id);
        Task FinalizarAsync(int id);
        Task AtualizarDataFimAsync(int id, DateTimeOffset novaDataFim);
    }
}
