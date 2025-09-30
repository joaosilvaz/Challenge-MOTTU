using Challenge_MOTTU.Model;

namespace Challenge_MOTTU.Services.Abstractions
{
    public interface IBikeService
    {

        Task<Bike> CriarAsync(Bike bike);

        Task<IEnumerable<Bike>> GetAllAsync();

        Task<Bike> GetById(int id);

        Task AtualizarDisponibilidadeAsync(int id, bool disponivel);

        Task DeletarAsync(int id);
    }
}
