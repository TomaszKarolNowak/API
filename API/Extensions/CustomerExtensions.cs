using API.Models;

namespace API.Extensions
{
    public static class CustomerExtensions
    {
        public static void ConvertToDto(this CustomerDto customer, CustomerInput input)
        {
            customer.Name = input.Name;
            customer.T = input.T;
            customer.V = input.V;
        }
    }
}
