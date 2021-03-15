using API.Context;
using API.Models;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace APITests.ServicesTests.CustomerServiceTests
{
    public class GetCustomersTests
    {
        private readonly Mock<CustomerContext> _context;
        private readonly Mock<DbSet<CustomerDto>> _dbSet;

        public CustomerService Service { get; set; }

        public GetCustomersTests()
        {
            _context = new Mock<CustomerContext>();
            _dbSet = new Mock<DbSet<CustomerDto>>();
            Service = new CustomerService(_context.Object);
        }

        [Fact]
        public async Task Given_NoCustomers_When_GetCustomersIsCalled_Then_NoCustomerIsReturned()
        {
            // Arrange
            var expected = new List<CustomerDto>();
            _dbSet.Setup(set => set.ToListAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(expected));

            // Act
            var actual = await Service.GetCustomers();

            // Assert
            Assert.Empty(actual);
        }

        [Fact]
        public async Task Given_One_When_GetCustomersIsCalled_Then_CustomerIsReturned()
        {
            // Arrange


            // Act


            // Assert
        }

        [Fact]
        public async Task Given_MultipleCustomers_When_GetCustomersIsCalled_Then_CustomersAreReturned()
        {
            // Arrange


            // Act


            // Assert
        }
    }
}
