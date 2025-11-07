using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Challenge_MOTTU.Services;
using Challenge_MOTTU.Model;
using Challenge_MOTTU.Exceptions;
using Challenge_MOTTU.Connection;

namespace Challenge_MOTTU.Tests
{
    public class BikeServiceTests
    {
        private AppDbContext SetupDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            var dbContext = new AppDbContext(options);
            dbContext.Database.EnsureCreated(); 
            return dbContext;
        }

        // Teste para quando os dados são válidos
        [Fact]
        public async Task CriarAsync_Bike_ValidData_ReturnsBike()
        {
            var dbContext = SetupDbContext();
            var bikeService = new BikeService(dbContext);  

            var bike = new Bike
            {
                Modelo = "BMW",
                Placa = "XYZ1234",
                Chassi = "XYZ5678",
                Ano = 2023,
                Disponivel = true
            };

            var result = await bikeService.CriarAsync(bike);

            Assert.Equal("BMW", result.Modelo);
            Assert.True(result.Disponivel);
            Assert.NotEqual(0, result.Id);
        }

        // Teste para quando a placa estiver vazia
        [Fact]
        public async Task CriarAsync_Bike_InvalidPlaca_ThrowsException()
        {
            var dbContext = SetupDbContext();
            var bikeService = new BikeService(dbContext);

            var bike = new Bike
            {
                Modelo = "BMW",
                Placa = "", 
                Chassi = "XYZ5678",
                Ano = 2023,
                Disponivel = true
            };

            await Assert.ThrowsAsync<BikeNotFoundException>(async () => await bikeService.CriarAsync(bike));
        }

        // Teste para quando o chassi já estiver em uso
        [Fact]
        public async Task CriarAsync_Bike_DuplicateChassi_ThrowsException()
        {
            var dbContext = SetupDbContext();
            var bikeService = new BikeService(dbContext);

            var bike1 = new Bike
            {
                Modelo = "BMW",
                Placa = "XYZ1234",
                Chassi = "CH123",
                Ano = 2023,
                Disponivel = true
            };

            var bike2 = new Bike
            {
                Modelo = "Yamaha",
                Placa = "ABC9876",
                Chassi = "CH123",
                Ano = 2022,
                Disponivel = true
            };

            dbContext.Bikes.Add(bike1);
            await dbContext.SaveChangesAsync();

            await Assert.ThrowsAsync<BikeNotFoundException>(async () => await bikeService.CriarAsync(bike2));
        }

        // Teste para quando a moto não for encontrada no banco (por ID)
        [Fact]
        public async Task GetById_Bike_NotFound_ThrowsException()
        {
            var dbContext = SetupDbContext();
            var bikeService = new BikeService(dbContext);

            await Assert.ThrowsAsync<BikeNotFoundException>(async () => await bikeService.GetById(999)); // ID inexistente
        }

        // Teste para atualizar a disponibilidade de uma moto
        [Fact]
        public async Task AtualizarDisponibilidade_ValidBike_UpdatesSuccessfully()
        {
            var dbContext = SetupDbContext();
            var bikeService = new BikeService(dbContext);

            var bike = new Bike
            {
                Modelo = "BMW",
                Placa = "XYZ1234",
                Chassi = "XYZ5678",
                Ano = 2023,
                Disponivel = true
            };

            dbContext.Bikes.Add(bike);
            await dbContext.SaveChangesAsync();

            await bikeService.AtualizarDisponibilidadeAsync(bike.Id, false);

            var updatedBike = await bikeService.GetById(bike.Id);
            Assert.False(updatedBike.Disponivel);  
        }

        // Teste para quando a moto não for encontrada ao tentar excluir
        [Fact]
        public async Task DeletarAsync_Bike_NotFound_ThrowsException()
        {
            var dbContext = SetupDbContext();
            var bikeService = new BikeService(dbContext);

            await Assert.ThrowsAsync<BikeNotFoundException>(async () => await bikeService.DeletarAsync(999));
        }
    }
}
