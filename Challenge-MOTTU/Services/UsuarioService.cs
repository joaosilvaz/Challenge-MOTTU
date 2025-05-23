using Challenge_MOTTU.Connection;
using Challenge_MOTTU.Exceptions;
using Challenge_MOTTU.Interfaces;
using Challenge_MOTTU.Model;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace Challenge_MOTTU.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _usuarioService;

        public UsuarioService(AppDbContext context)
        {
            _usuarioService = context;
        }

        public async Task<Usuario> CriarAsync(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nome))
                throw new UsuarioException("O campo Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(usuario.Email))
                 throw new UsuarioException("O campo Email é obrigatório.");

            var emailEmUso = _usuarioService.Usuarios.FirstOrDefault(p => p.Email.Trim() == usuario.Email.Trim());

            if (emailEmUso != null)
                throw new UsuarioException("O email informado já está em uso.");

            if (string.IsNullOrWhiteSpace(usuario.Senha))
                throw new UsuarioException("O campo Senha é obrigatório.");

            _usuarioService.Usuarios.Add(usuario);

            _usuarioService.SaveChanges();

            return  usuario;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            var usuarios = await _usuarioService.Usuarios.ToListAsync();

            if (usuarios == null || !usuarios.Any())
                throw new UsuarioException("Nenhum usuário encontrado.");

            return usuarios;
        }

        public async Task<Usuario> GetByEmail(string? email)
        {
            if (string.IsNullOrEmpty(email))
                throw new UsuarioException("E-mail está vazio");

            Usuario? usuario = _usuarioService.Usuarios.FirstOrDefault(p => p.Email == email);

            if (usuario is null)
                throw new UsuarioException("Usuário com este e-mail não existe");

            return usuario;
        }

        public async Task<Usuario> GetById(int id)
        {
            var usuario = _usuarioService.Usuarios.FirstOrDefault(u => u.Id == id);

            if (usuario is null)
                throw new UsuarioException();

            return usuario;
        }

        public async Task<Usuario> AtualizarAsync(int id, Usuario usuarioAtualizado)
        {
            var usuario = await _usuarioService.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
                throw new UsuarioException("Usuário não encontrado.");

            usuario.Nome = usuarioAtualizado.Nome;
            usuario.Email = usuarioAtualizado.Email;
            usuario.Senha = usuarioAtualizado.Senha;

            await _usuarioService.SaveChangesAsync();
            return usuario;
        }

        public async Task DeletarAsync(int id)
        {
            var usuario = await _usuarioService.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
                throw new UsuarioException("Usuário não encontrado.");

            _usuarioService.Usuarios.Remove(usuario);
            await _usuarioService.SaveChangesAsync();
        }
    }
}
