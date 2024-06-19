using JediApi.Models;
using JediApi.Repositories;
using JediApi.Services;
using Moq;

namespace JediApi.Tests.Services
{
    public class JediServiceTests
    {
        // não mexer
        private readonly JediService _service;
        private readonly Mock<IJediRepository> _repositoryMock;
        private object _mockJediRepository; //add isso aqui pq apareceu no fix
        private readonly JediRepository _repository; 

        public JediServiceTests()
        {
            // não mexer
            _repositoryMock = new Mock<IJediRepository>();
            _service = new JediService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetById_Success()
        {
            Jedi sepcted = new Jedi { Id = 1, Name = "Luke Skywalker", Strength = 100, Version = 1 };

            var jedi = await _repository.GetByIdAsync(1);

            Assert.NotNull(jedi);
            Assert.Equal(2, jedi.Id);
            Assert.Equal("Han Solo", jedi.Name);
            Assert.Equal(1, jedi.Version);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
                var result = await _service.GetByIdAsync(1);

                Assert.Null(result);
        }

        [Fact]
        public async Task GetAll()
        {
            var jedis = new List<Jedi>
            {
                new Jedi { Id = 1, Name = "Luke Skywalker",Strength = 100, Version = 1 },
                new Jedi { Id = 2, Name = "Han Solo", Strength = 80, Version = 1 }
            };

            var result = await _repository.GetAllAsync();

            Assert.Equal(2, jedis.Count);
            Assert.Equal(1, result.Count);
            Assert.Equal("Luke Skywalker", result[0].Name);
            Assert.Equal("Yoda", result[1].Name);
        }
    }
}
