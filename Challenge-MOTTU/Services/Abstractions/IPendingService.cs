using Challenge_MOTTU.Model;

namespace Challenge_MOTTU.Services.Abstractions
{
    public interface IPendingService
    {
        Task<Pending> CriarAsync(Pending pending);

        Task<IEnumerable<Pending>> GetAllAsync();

        Task<Pending> GetById(int id);

        Task<Pending> AtualizarAsync(int id, Pending pendingAtualizada);

        Task DeletarAsync(int id);
    }
}
