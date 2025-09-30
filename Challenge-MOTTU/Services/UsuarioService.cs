using Challenge_MOTTU.Connection;
using Challenge_MOTTU.Exceptions;
using Challenge_MOTTU.Model;
using Challenge_MOTTU.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Challenge_MOTTU.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> CriarAsync(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nome))
                throw new UsuarioNotFoundException("O campo Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(usuario.Email))
                throw new UsuarioNotFoundException("O campo Email é obrigatório.");

            var emailEmUso = await _context.Usuarios.FirstOrDefaultAsync(p => p.Email.Trim() == usuario.Email.Trim());
            if (emailEmUso != null)
                throw new UsuarioNotFoundException("O email informado já está em uso.");

            if (string.IsNullOrWhiteSpace(usuario.Senha))
                throw new UsuarioNotFoundException("O campo Senha é obrigatório.");

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            if (!usuarios.Any())
                throw new UsuarioNotFoundException("Nenhum usuário encontrado.");

            return usuarios;
        }

        public async Task<Usuario> GetByEmail(string? email)
        {
            if (string.IsNullOrEmpty(email))
                throw new UsuarioNotFoundException("E-mail está vazio");

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(p => p.Email == email);
            if (usuario is null)
                throw new UsuarioNotFoundException("Usuário com este e-mail não existe");

            return usuario;
        }

        public async Task<Usuario> GetById(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario is null)
                throw new UsuarioNotFoundException();

            return usuario;
        }

        public async Task<Usuario> AtualizarAsync(int id, Usuario usuarioAtualizado)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
                throw new UsuarioNotFoundException("Usuário não encontrado.");

            usuario.Nome = usuarioAtualizado.Nome;
            usuario.Email = usuarioAtualizado.Email;
            usuario.Senha = usuarioAtualizado.Senha;

            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task DeletarAsync(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
                throw new UsuarioNotFoundException("Usuário não encontrado.");

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
