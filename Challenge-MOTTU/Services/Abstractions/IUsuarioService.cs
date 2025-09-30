using Challenge_MOTTU.Model;

namespace Challenge_MOTTU.Services.Abstractions
{
    public interface IUsuarioService
    {
        Task<Usuario> CriarAsync(Usuario usuario);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetById(int id);
        Task<Usuario> GetByEmail(string email);
        Task<Usuario> AtualizarAsync(int id, Usuario usuarioAtualizado);
        Task DeletarAsync(int id);
    }
}
