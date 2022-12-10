using FindTheBuilder.Applications.Services.CustomerAppServices;
using FindTheBuilder.Applications.Services.CustomerAppServices.DTO;
using FindTheBuilder.Databases.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Data.Common;

namespace FindTheBuilder.UnitTest.ServiceTest
{
    public class CustomerAppServiceTest : IClassFixture<Startup>
    {
        private ServiceProvider _serviceProvider;
        public CustomerAppServiceTest(Startup fixtur)
        {
            _serviceProvider = fixtur.ServiceProvider;
        }

        [Fact]
        public void CreateCustomer()
        {
            var service = _serviceProvider.GetService<ICustomerAppService>();

            CustomerDTO newCustomer = new CustomerDTO()
            {
                Name = "Test",
                Address = "Ragunan, Jakarta Selatan",
                Phone = "081654732985"
            };

            var result = service.Create(newCustomer);
            Assert.NotNull(result);
        }

        [Fact]
        public void UpdateCustomer()
        {
            var service = _serviceProvider.GetService<ICustomerAppService>();

            UpdateCustomerDTO updateCustomer = new UpdateCustomerDTO()
            {
                Id = 1,
                Name = "Test",
                Address = "Ragunan, Jakarta Selatan",
                Phone = "081654732985"
            };

            var result = service.Update(updateCustomer);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetCustomerByName()
        {
            var service = _serviceProvider.GetService<ICustomerAppService>();

            var customer = service.GetByName("Test");
            Assert.NotNull(customer);
        }
    }
}