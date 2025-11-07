using System;
using System.Threading.Tasks;
using Challenge_MOTTU.Connection;
using Challenge_MOTTU.Exceptions;
using Challenge_MOTTU.Model;
using Challenge_MOTTU.Services;
using Challenge_MOTTU.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Challenge_MOTTU.Tests
{
    public class PendingServiceTests
    {
        private AppDbContext SetupDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_Pendings") 
                .Options;

            var dbContext = new AppDbContext(options);
            dbContext.Database.EnsureCreated();
            return dbContext;
        }

        [Fact]
        public async Task CriarAsync_Pending_ValidData_ReturnsPending()
        {
            var dbContext = SetupDbContext();

            var bike = new Bike { Id = 1, Modelo = "BMW", Placa = "XYZ1234", Chassi = "ABC1234", Disponivel = true };
            var usuario = new Usuario { Id = 1, Nome = "Gabriel", Email = "gabriel@email.com" };

            dbContext.Bikes.Add(bike);
            dbContext.Usuarios.Add(usuario);
            await dbContext.SaveChangesAsync();

            var pendingService = new PendingService(dbContext);

            var pending = new Pending
            {
                UsuarioId = usuario.Id,
                BikeId = bike.Id,
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now.AddDays(1)
            };

            // Chama o método CriarAsync
            var result = await pendingService.CriarAsync(pending);

            // Verifica o resultado
            Assert.NotNull(result);
            Assert.Equal(pending.UsuarioId, result.UsuarioId);
            Assert.Equal(pending.BikeId, result.BikeId);
            Assert.Equal(Enums.StatusAluguel.Pendente, result.Status);  
        }

        [Fact]
        public async Task CriarAsync_InvalidBikeId_ThrowsException()
        {
            var dbContext = SetupDbContext();
            var pendingService = new PendingService(dbContext);

            var invalidPending = new Pending
            {
                UsuarioId = 1,
                BikeId = -1,
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now.AddDays(1)
            };

            await Assert.ThrowsAsync<PendingNotFoundException>(async () => await pendingService.CriarAsync(invalidPending));
        }

        [Fact]
        public async Task CriarAsync_DuplicatePending_ThrowsException()
        {
            // Configura o banco em memória e cria o contexto
            var dbContext = SetupDbContext();

            // Criação de bike e usuário
            var bike = new Bike { Id = 2, Modelo = "HYUNDAI", Placa = "XYE3234", Chassi = "ABC3334", Disponivel = true };
            var usuario = new Usuario { Id = 2, Nome = "Gabriel", Email = "gabriel@email.com" };

            dbContext.Bikes.Add(bike);
            dbContext.Usuarios.Add(usuario);
            await dbContext.SaveChangesAsync();

            var pendingService = new PendingService(dbContext);

            // Criação de pendência com o mesmo usuário e bike
            var pending1 = new Pending
            {
                UsuarioId = usuario.Id,
                BikeId = bike.Id,
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now.AddDays(1)
            };

            await pendingService.CriarAsync(pending1); // Cria a primeira pendência

            var pending2 = new Pending
            {
                UsuarioId = usuario.Id,
                BikeId = bike.Id,
                DataInicio = DateTime.Now.AddDays(2),
                DataFim = DateTime.Now.AddDays(3)
            };

            await Assert.ThrowsAsync<PendingNotFoundException>(async () => await pendingService.CriarAsync(pending2));
        }
    }
}
