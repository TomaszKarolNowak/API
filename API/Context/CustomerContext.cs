using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerDto> Customers { get; set; }
    }
}
