using FindTheBuilder.Applications.Services.CustomerAppServices;
using FindTheBuilder.Applications.Services.CustomerAppServices.DTO;
using FindTheBuilder.Applications.Services.PriceAppServices;
using FindTheBuilder.Applications.Services.TransactionAppServices;
using FindTheBuilder.Applications.Services.TransactionDetailAppServices;
using FindTheBuilder.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.UnitTest.ControllerTest
{
	public class CustomerControllerTest : IClassFixture<Startup>
	{
		private ServiceProvider _serviceProvider;
		public CustomerControllerTest(Startup fixture)
		{
			_serviceProvider = fixture.ServiceProvider;
		}

		[Fact]
		public void CreateCustomerTestSuccess()
		{
			var service = _serviceProvider.GetService<CustomerController>();
			//Arrange
			CustomerDTO model = new CustomerDTO()
			{
				Name = "Mahdi",
				Address = "Magelang",
				Phone = "089897655677"
			};

			//Act
			var result = service.CreateCustomer(model) as ObjectResult;

			//Assert
			Assert.NotNull(result.StatusCode);
		}

	}
}
