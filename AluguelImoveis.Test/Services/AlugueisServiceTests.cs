using AluguelImoveis.Models;
using AluguelImoveis.Repositories.Interfaces;
using Moq;
using Xunit;
using AluguelImoveis.Services;

namespace AluguelImoveis.Tests.Services
{
    public class AluguelServiceTests
    {
        private readonly Mock<IAluguelRepository> _mockAluguelRepository;
        private readonly Mock<IImovelRepository> _mockImovelRepository;
        private readonly Mock<ILocatarioRepository> _mockLocatarioRepository;
        private readonly AluguelService _service;

        public AluguelServiceTests()
        {
            _mockLocatarioRepository = new Mock<ILocatarioRepository>();
            _mockImovelRepository = new Mock<IImovelRepository>();
            _mockAluguelRepository = new Mock<IAluguelRepository>();
            _service = new AluguelService(
                _mockAluguelRepository.Object,
                _mockImovelRepository.Object,
                _mockLocatarioRepository.Object
            );
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllAlugueis()
        {
            // Arrange
            var expected = new List<Aluguel> { new Aluguel(), new Aluguel() };
            _mockAluguelRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(expected);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ShouldReturnAluguel()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expected = new Aluguel { Id = id };
            _mockAluguelRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(expected);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task CreateAsync_WithUnavailableImovel_ShouldThrowException()
        {
            // Arrange
            var aluguel = new Aluguel
            {
                DataInicio = DateTime.Today,
                DataTermino = DateTime.Today.AddDays(1),
                ImovelId = Guid.NewGuid()
            };

            _mockImovelRepository
                .Setup(x => x.GetByIdAsync(aluguel.ImovelId))
                .ReturnsAsync(new Imovel { Disponivel = false });

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _service.CreateAsync(aluguel)
            );
        }

        [Fact]
        public async Task DeleteAsync_WithValidId_ShouldDeleteAluguel()
        {
            // Arrange
            var id = Guid.NewGuid();
            var aluguel = new Aluguel { Id = id, ImovelId = Guid.NewGuid() };

            _mockAluguelRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(aluguel);
            _mockImovelRepository
                .Setup(x => x.GetByIdAsync(aluguel.ImovelId))
                .ReturnsAsync(new Imovel());

            // Act
            await _service.DeleteAsync(id);

            // Assert
            _mockAluguelRepository.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var aluguel = new Aluguel { Id = Guid.NewGuid() };

            // Act
            await _service.UpdateAsync(aluguel);

            // Assert
            _mockAluguelRepository.Verify(x => x.UpdateAsync(aluguel), Times.Once);
        }

        [Fact]
        public async Task GetAllDetailedAsync_ShouldReturnList()
        {
            // Arrange
            var alugueis = new List<Aluguel> { new Aluguel() };
            _mockAluguelRepository.Setup(x => x.GetAllDetailedAsync()).ReturnsAsync(alugueis);

            // Act
            var result = await _service.GetAllDetailedAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }
    }
}
