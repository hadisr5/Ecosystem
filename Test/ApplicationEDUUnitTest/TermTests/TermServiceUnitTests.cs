using Seventy.Data;
using Seventy.DomainClass.EDU.Term;
using Seventy.Repository.Core;
using Seventy.Repository.Core.Repositories;
using Seventy.Repository.Persistence;
using Seventy.Repository.Persistence.Repositories;
using Seventy.Service.EDU.Term;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationEDUUnitTest.TermTests
{
    public class TermServiceUnitTests
    {
        private readonly DataContext _dbContext;
        private readonly ITermRepository _termRepository;
        private readonly ITermService _termService;
        private readonly IUnitOfWork _unitOfWork;

        public TermServiceUnitTests()
        {
            //_dbContext = new InMemoryDbContextFactory().GetInMemoryFakeDataContext();
            _termRepository = new TermRepository(_dbContext);
            _unitOfWork = new UnitOfWork(_dbContext);
            _termService = new TermService(_unitOfWork);
        }

        [Fact]
        public async Task GetTermById_ValidTermIdIsReturned()
        {
            // Arrange
            await _termService.InsertRangeAsync(new Term[]
            {
                new Term { ID = 1, Title = "Computer" },
                new Term { ID = 2, Title = "Physic" },
                new Term { ID = 3, Title = "Mathmatic" },
            }, new CancellationToken());
            await _dbContext.SaveChangesAsync();

            // Act
            var actual = await _termRepository.GetByIDAsync(new CancellationToken(), 3);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal<int>(3, actual.ID.Value);
        }
    }
}
