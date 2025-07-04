using AluguelImoveis.Models;
using AluguelImoveis.Repositories.Interfaces;
using AluguelImoveis.Services;
using Moq;
using Xunit;

namespace AluguelImoveis.Test.Services
{
    public class ImovelServiceTests
    {
        private readonly Mock<IImovelRepository> _mockImovelRepository;
        private readonly Mock<IAluguelRepository> _mockAluguelRepository;
        private readonly ImovelService _service;

        public ImovelServiceTests()
        {
            _mockImovelRepository = new Mock<IImovelRepository>();
            _mockAluguelRepository = new Mock<IAluguelRepository>();
            _service = new ImovelService(
                _mockImovelRepository.Object,
                _mockAluguelRepository.Object
            );
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllImoveis()
        {
            // Arrange
            var imoveis = new List<Imovel>
            {
                new Imovel { Id = Guid.NewGuid(), Codigo = "CASA-001" },
                new Imovel { Id = Guid.NewGuid(), Codigo = "APT-002" }
            };
            _mockImovelRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(imoveis);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
            _mockImovelRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnImovel_WhenExists()
        {
            // Arrange
            var imovelId = Guid.NewGuid();
            var imovel = new Imovel { Id = imovelId, Codigo = "CASA-001" };
            _mockImovelRepository.Setup(repo => repo.GetByIdAsync(imovelId)).ReturnsAsync(imovel);

            // Act
            var result = await _service.GetByIdAsync(imovelId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(imovelId, result.Id);
            _mockImovelRepository.Verify(repo => repo.GetByIdAsync(imovelId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            // Arrange
            var imovelId = Guid.NewGuid();
            _mockImovelRepository
                .Setup(repo => repo.GetByIdAsync(imovelId))
                .ReturnsAsync((Imovel?)null);

            // Act
            var result = await _service.GetByIdAsync(imovelId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenCodigoExists()
        {
            // Arrange
            var imovel = new Imovel { Codigo = "CASA-001" };

            _mockImovelRepository
                .Setup(repo => repo.CodigoExistsAsync(imovel.Codigo, null)) 
                .ReturnsAsync(true);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => _service.CreateAsync(imovel)
            );

            Assert.Equal("Já existe um imóvel com este código cadastrado", exception.Message);

            _mockImovelRepository.Verify(
                repo => repo.CodigoExistsAsync(imovel.Codigo, null),
                Times.Once
            );
        }

        [Fact]
        public async Task CreateAsync_ShouldAddImovel_WhenCodigoIsUnique()
        {
            // Arrange
            var imovel = new Imovel { Codigo = "CASA-001" };

            _mockImovelRepository
                .Setup(repo => repo.CodigoExistsAsync(imovel.Codigo, null))
                .ReturnsAsync(false);

            _mockImovelRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Imovel>()))
                .ReturnsAsync(imovel);

            // Act
            var result = await _service.CreateAsync(imovel);

            // Assert
            Assert.Equal(imovel.Codigo, result.Codigo);

            _mockImovelRepository.Verify(
                repo => repo.CodigoExistsAsync(imovel.Codigo, null),
                Times.Once
            );
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenImovelNotFound()
        {
            // Arrange
            var imovel = new Imovel { Id = Guid.NewGuid(), Codigo = "CASA-001" };
            _mockImovelRepository
                .Setup(repo => repo.GetByIdAsync(imovel.Id))
                .ReturnsAsync((Imovel?)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateAsync(imovel));
            _mockImovelRepository.Verify(repo => repo.UpdateAsync(imovel), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenCodigoExistsForAnotherImovel()
        {
            // Arrange
            var imovel = new Imovel { Id = Guid.NewGuid(), Codigo = "CASA-001" };
            var existingImovel = new Imovel { Id = Guid.NewGuid(), Codigo = "CASA-002" };
            _mockImovelRepository
                .Setup(repo => repo.GetByIdAsync(imovel.Id))
                .ReturnsAsync(existingImovel);
            _mockImovelRepository
                .Setup(repo => repo.CodigoExistsAsync(imovel.Codigo, imovel.Id))
                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.UpdateAsync(imovel));
            _mockImovelRepository.Verify(repo => repo.UpdateAsync(imovel), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdate_WhenValid()
        {
            // Arrange
            var imovel = new Imovel { Id = Guid.NewGuid(), Codigo = "CASA-001" };
            var existingImovel = new Imovel { Id = imovel.Id, Codigo = "CASA-002" };
            _mockImovelRepository
                .Setup(repo => repo.GetByIdAsync(imovel.Id))
                .ReturnsAsync(existingImovel);
            _mockImovelRepository
                .Setup(repo => repo.CodigoExistsAsync(imovel.Codigo, imovel.Id))
                .ReturnsAsync(false);

            // Act
            await _service.UpdateAsync(imovel);

            // Assert
            _mockImovelRepository.Verify(repo => repo.UpdateAsync(imovel), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenImovelNotFound()
        {
            // Arrange
            var imovelId = Guid.NewGuid();
            _mockImovelRepository
                .Setup(repo => repo.GetByIdAsync(imovelId))
                .ReturnsAsync((Imovel?)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteAsync(imovelId));
            _mockImovelRepository.Verify(repo => repo.DeleteAsync(imovelId), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenImovelHasAlugueis()
        {
            // Arrange
            var imovelId = Guid.NewGuid();
            var imovel = new Imovel { Id = imovelId };
            _mockImovelRepository.Setup(repo => repo.GetByIdAsync(imovelId)).ReturnsAsync(imovel);
            _mockAluguelRepository
                .Setup(repo => repo.ExistsForImovelAsync(imovelId))
                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _service.DeleteAsync(imovelId)
            );
            _mockImovelRepository.Verify(repo => repo.DeleteAsync(imovelId), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDelete_WhenNoAlugueis()
        {
            // Arrange
            var imovelId = Guid.NewGuid();
            var imovel = new Imovel { Id = imovelId };
            _mockImovelRepository.Setup(repo => repo.GetByIdAsync(imovelId)).ReturnsAsync(imovel);
            _mockAluguelRepository
                .Setup(repo => repo.ExistsForImovelAsync(imovelId))
                .ReturnsAsync(false);

            // Act
            await _service.DeleteAsync(imovelId);

            // Assert
            _mockImovelRepository.Verify(repo => repo.DeleteAsync(imovelId), Times.Once);
        }

        [Fact]
        public async Task GetDisponiveisAsync_ShouldReturnOnlyDisponiveis()
        {
            // Arrange
            var imoveis = new List<Imovel>
            {
                new Imovel
                {
                    Id = Guid.NewGuid(),
                    Codigo = "CASA-001",
                    Disponivel = true
                },
                new Imovel
                {
                    Id = Guid.NewGuid(),
                    Codigo = "APT-002",
                    Disponivel = false
                }
            };
            _mockImovelRepository
                .Setup(repo => repo.GetDisponiveisAsync())
                .ReturnsAsync(imoveis.Where(i => i.Disponivel));

            // Act
            var result = await _service.GetDisponiveisAsync();

            // Assert
            Assert.Single(result);
            Assert.All(result, imovel => Assert.True(imovel.Disponivel));
        }
    }
}
