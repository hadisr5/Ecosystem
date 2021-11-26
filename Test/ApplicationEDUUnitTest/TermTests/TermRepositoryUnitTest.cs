using Microsoft.EntityFrameworkCore;
using Seventy.Data;
using Seventy.DomainClass.EDU.Term;
using Seventy.Repository.Core.Repositories;
using Seventy.Repository.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationEDUUnitTest.TermTests
{
    public class TermRepositoryUnitTest
    {
        private readonly DataContext _dbContext;
        private readonly ITermRepository _termRepository;

        public TermRepositoryUnitTest()
        {
            //_dbContext = new InMemoryDbContextFactory().GetInMemoryFakeDataContext();
            _termRepository = new TermRepository(_dbContext);
        }

        [Fact]
        public async Task GetTermById_ValidTermIdIsReturned()
        {
            // Arrange
            await _termRepository.InsertRangeAsync(new Term[] 
            { 
                new Term { ID = 1, Title = "Computer" },
                new Term { ID = 2, Title = "Physic" },
                new Term { ID = 3, Title = "Mathmatic" },
            }, new CancellationToken());
            await _dbContext.SaveChangesAsync();

            // Act
            var actual = await _termRepository.GetByIDAsync(new CancellationToken(), 1);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal<int>(1, actual.ID.Value);
        }
    }
}
