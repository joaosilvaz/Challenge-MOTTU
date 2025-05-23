using Challenge_MOTTU.Model;

namespace Challenge_MOTTU.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> CriarAsync(Usuario usuario);

        Task<Usuario> GetById(int id);

        Task<Usuario> GetByEmail(string email);

        Task<IEnumerable<Usuario>> GetAllAsync();

        Task<Usuario> AtualizarAsync(int id, Usuario usuarioAtualizado);

        Task DeletarAsync(int id);
    }
}