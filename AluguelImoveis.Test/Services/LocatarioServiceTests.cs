using AluguelImoveis.Models;
using AluguelImoveis.Repositories.Interfaces;
using AluguelImoveis.Services;
using Moq;
using Xunit;

namespace AluguelImoveis.Test.Services
{
    public class LocatarioServiceTests
    {
        private readonly Mock<ILocatarioRepository> _mockLocatarioRepository;
        private readonly Mock<IAluguelRepository> _mockAluguelRepository;
        private readonly LocatarioService _service;

        public LocatarioServiceTests()
        {
            _mockLocatarioRepository = new Mock<ILocatarioRepository>();
            _mockAluguelRepository = new Mock<IAluguelRepository>();
            _service = new LocatarioService(
                _mockLocatarioRepository.Object,
                _mockAluguelRepository.Object
            );
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllLocatarios()
        {
            // Arrange
            var expected = new List<Locatario>
            {
                new Locatario
                {
                    Id = Guid.NewGuid(),
                    NomeCompleto = "Locatario 1",
                    CPF = "12345678901"
                },
                new Locatario
                {
                    Id = Guid.NewGuid(),
                    NomeCompleto = "Locatario 2",
                    CPF = "10987654321"
                }
            };

            _mockLocatarioRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expected);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(expected, result);
            _mockLocatarioRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ShouldReturnLocatario()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expected = new Locatario
            {
                Id = id,
                NomeCompleto = "Locatario Teste",
                CPF = "12345678901"
            };

            _mockLocatarioRepository
                .Setup(repo => repo.GetByIdAsync(id))
                .ReturnsAsync(expected);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.Equal(expected, result);
            _mockLocatarioRepository.Verify(repo => repo.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            var id = Guid.NewGuid();

            _mockLocatarioRepository
                .Setup(repo => repo.GetByIdAsync(id))
                .ReturnsAsync((Locatario?)null);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.Null(result);
            _mockLocatarioRepository.Verify(repo => repo.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WithUniqueCpf_ShouldCreateLocatario()
        {
            // Arrange
            var newLocatario = new Locatario
            {
                NomeCompleto = "Novo Locatario",
                CPF = "12345678901"
            };

            _mockLocatarioRepository
                .Setup(repo => repo.CpfExistsAsync(newLocatario.CPF))
                .ReturnsAsync(false);
            _mockLocatarioRepository
                .Setup(repo => repo.AddAsync(newLocatario))
                .ReturnsAsync(newLocatario);

            // Act
            var result = await _service.CreateAsync(newLocatario);

            // Assert
            Assert.Equal(newLocatario, result);
            _mockLocatarioRepository.Verify(
                repo => repo.CpfExistsAsync(newLocatario.CPF),
                Times.Once
            );
            _mockLocatarioRepository.Verify(repo => repo.AddAsync(newLocatario), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WithValidData_ShouldUpdateLocatario()
        {
            // Arrange
            var existingId = Guid.NewGuid();
            var existingLocatario = new Locatario
            {
                Id = existingId,
                NomeCompleto = "Locatario Antigo",
                CPF = "12345678901"
            };
            var updatedLocatario = new Locatario
            {
                Id = existingId,
                NomeCompleto = "Locatario Atualizado",
                CPF = "12345678901"
            };

            _mockLocatarioRepository
                .Setup(repo => repo.GetByIdAsync(existingId))
                .ReturnsAsync(existingLocatario);

            // Act
            await _service.UpdateAsync(updatedLocatario);

            // Assert
            _mockLocatarioRepository.Verify(repo => repo.GetByIdAsync(existingId), Times.Once);
            _mockLocatarioRepository.Verify(
                repo => repo.CpfExistsAsync(It.IsAny<string>()),
                Times.Never
            );
            _mockLocatarioRepository.Verify(repo => repo.UpdateAsync(updatedLocatario), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WithValidIdAndNoAlugueis_ShouldDeleteLocatario()
        {
            // Arrange
            var id = Guid.NewGuid();
            var locatario = new Locatario
            {
                Id = id,
                NomeCompleto = "Locatario",
                CPF = "12345678901"
            };

            _mockLocatarioRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(locatario);
            _mockAluguelRepository
                .Setup(repo => repo.ExistsForLocatarioAsync(id))
                .ReturnsAsync(false);

            // Act
            await _service.DeleteAsync(id);

            // Assert
            _mockLocatarioRepository.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _mockAluguelRepository.Verify(repo => repo.ExistsForLocatarioAsync(id), Times.Once);
            _mockLocatarioRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }
    }
}
