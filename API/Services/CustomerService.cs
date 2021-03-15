using API.Context;
using API.Extensions;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerContext _context;

        public CustomerService(CustomerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomers(long? timestampFrom = null, long? timestampTo = null)
        {
            if (timestampFrom.HasValue && !timestampTo.HasValue)
            {
                throw new ArgumentNullException(nameof(timestampTo), "Both values need to be null or specified.");
            }

            if (!timestampFrom.HasValue && timestampTo.HasValue)
            {
                throw new ArgumentNullException(nameof(timestampFrom), "Both values need to be null or specified.");
            }

            if (!timestampFrom.HasValue && !timestampTo.HasValue)
            {
                return await _context.Customers.ToListAsync();
            }

            if (timestampFrom > timestampTo)
            {
                throw new ArgumentOutOfRangeException(nameof(timestampFrom), "Starting timestamp is greater than end timestamp");
            }

            if (timestampFrom < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timestampFrom), "The value must bed greater than 0");
            }

            if (timestampTo < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timestampTo), "The value must bed greater than 0");
            }

            var customers = (await _context.Customers.ToListAsync())
                .FindAll(customer => customer.T >= timestampFrom && customer.T <= timestampTo);

            return customers;
        }

        public async Task<CustomerDto> GetCustomerById(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<CustomerDto> PostCustomer(CustomerInput input)
        {
            var customer = new CustomerDto();
            customer.ConvertToDto(input);

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<CustomerDto> PutCustomer(int id, CustomerInput input)
        {
            var customer = await GetCustomerById(id);
            _context.Entry(customer).State = EntityState.Modified;
            customer.ConvertToDto(input);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return customer;
        }

        public async Task<CustomerDto> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }

            return customer;
        }       
    }
}
