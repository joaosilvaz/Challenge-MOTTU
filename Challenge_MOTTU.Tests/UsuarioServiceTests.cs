using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Challenge_MOTTU.Exceptions;
using Challenge_MOTTU.Model;
using Challenge_MOTTU.Services;
using Challenge_MOTTU.Connection;
using System.Threading.Tasks;
using System.Linq;

namespace Challenge_MOTTU.Tests
{
    public class UsuarioServiceTests
    {
        // Configura o banco de dados em memória antes de cada teste
        private AppDbContext SetupDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString()) 
                .Options;

            var dbContext = new AppDbContext(options);
            dbContext.Database.EnsureCreated(); 
            return dbContext;
        }

        [Fact]
        public async Task CriarAsync_Usuario_ValidData_ReturnsUsuario()
        {
            var dbContext = SetupDbContext(); 
            var usuarioService = new UsuarioService(dbContext);

            var usuario = new Usuario
            {
                Nome = "Lucas Martins",
                Email = "lucas@email.com",
                Senha = "Senha123"
            };

            // Limpa o banco de dados antes de criar o novo usuário
            dbContext.Usuarios.RemoveRange(dbContext.Usuarios);
            await dbContext.SaveChangesAsync();

            var result = await usuarioService.CriarAsync(usuario); 

            Assert.NotNull(result); 
            Assert.Equal(usuario.Nome, result.Nome);
            Assert.Equal(usuario.Email, result.Email);
            Assert.Equal(usuario.Senha, result.Senha);
        }

        [Fact]
        public async Task CriarAsync_Usuario_DuplicateEmail_ThrowsException()
        {
            var dbContext = SetupDbContext();
            var usuarioService = new UsuarioService(dbContext);

            dbContext.Usuarios.RemoveRange(dbContext.Usuarios);  
            await dbContext.SaveChangesAsync();

            var usuarioExistente = new Usuario
            {
                Nome = "Lucas Martins",
                Email = "lucas@email.com",
                Senha = "Senha123"
            };
            dbContext.Usuarios.Add(usuarioExistente);
            await dbContext.SaveChangesAsync();

            var novoUsuario = new Usuario
            {
                Nome = "Carlos Eduardo",
                Email = "lucas@email.com",
                Senha = "Senha456"
            };

            await Assert.ThrowsAsync<UsuarioNotFoundException>(async () => await usuarioService.CriarAsync(novoUsuario)); 
        }
   

        // Teste para verificar se uma exceção é lançada quando o campo Nome está vazio
        [Fact]
        public async Task CriarAsync_Usuario_InvalidNome_ThrowsException()
        {
            var dbContext = SetupDbContext();
            var usuarioService = new UsuarioService(dbContext);

            var usuario = new Usuario
            {
                Nome = "", 
                Email = "lucas123@email.com",
                Senha = "Senha123"
            };

            await Assert.ThrowsAsync<UsuarioNotFoundException>(async () => await usuarioService.CriarAsync(usuario));
        }

        // Teste para o método GetById
        [Fact]
        public async Task GetById_Usuario_ValidId_ReturnsUsuario()
        {
            var dbContext = SetupDbContext();
            var usuarioService = new UsuarioService(dbContext);

            // Criação de um usuário no banco
            var usuario = new Usuario
            {
                Nome = "Lucas Martins",
                Email = "lucas123@email.com",
                Senha = "Senha123"
            };
            dbContext.Usuarios.Add(usuario);
            await dbContext.SaveChangesAsync();

            var result = await usuarioService.GetById(usuario.Id);

            Assert.NotNull(result);
            Assert.Equal(usuario.Nome, result.Nome);
            Assert.Equal(usuario.Email, result.Email);
            Assert.Equal(usuario.Senha, result.Senha);
        }

        // Teste para o método GetById com ID inválido
        [Fact]
        public async Task GetById_Usuario_InvalidId_ThrowsException()
        {
            var dbContext = SetupDbContext();
            var usuarioService = new UsuarioService(dbContext);

            await Assert.ThrowsAsync<UsuarioNotFoundException>(async () => await usuarioService.GetById(999));
        }

        // Teste para o método AtualizarAsync
        [Fact]
        public async Task AtualizarAsync_Usuario_ValidData_ReturnsUpdatedUsuario()
        {
            var dbContext = SetupDbContext();
            var usuarioService = new UsuarioService(dbContext);

            var usuario = new Usuario
            {
                Nome = "Lucas Martins",
                Email = "lucas123@email.com",
                Senha = "Senha123"
            };
            dbContext.Usuarios.Add(usuario);
            await dbContext.SaveChangesAsync();

            var usuarioAtualizado = new Usuario
            {
                Nome = "Lucas Silva",
                Email = "lucas.silva@email.com",
                Senha = "Senha321"
            };

            var result = await usuarioService.AtualizarAsync(usuario.Id, usuarioAtualizado);

            Assert.NotNull(result);
            Assert.Equal(usuarioAtualizado.Nome, result.Nome);
            Assert.Equal(usuarioAtualizado.Email, result.Email);
            Assert.Equal(usuarioAtualizado.Senha, result.Senha);
        }

        // Teste para o método DeletarAsync
        [Fact]
        public async Task DeletarAsync_Usuario_ValidId_DeletesUsuario()
        {
            var dbContext = SetupDbContext();
            var usuarioService = new UsuarioService(dbContext);

            var usuario = new Usuario
            {
                Nome = "Lucas Martins",
                Email = "lucas123@email.com",
                Senha = "Senha123"
            };
            dbContext.Usuarios.Add(usuario);
            await dbContext.SaveChangesAsync();

            await usuarioService.DeletarAsync(usuario.Id);

            var deletedUsuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == usuario.Id);
            Assert.Null(deletedUsuario);
        }
    }
}
