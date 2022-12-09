using FindTheBuilder.Applications.Services.CustomerAppServices.DTO;
using FindTheBuilder.Applications.Services.CustomerAppServices;
using FindTheBuilder.Applications.Services.PriceAppServices;
using FindTheBuilder.Applications.Services.PriceAppServices.DTO;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindTheBuilder.Applications.Services.TukangAppServices.DTO;
using FindTheBuilder.Applications.Services.TukangAppServices;

namespace FindTheBuilder.UnitTest
{
	public class PriceAppServiceTest: IClassFixture<Startup>
	{
		private ServiceProvider _serviceProvider;
		public PriceAppServiceTest(Startup fixtur)
		{
			_serviceProvider = fixtur.ServiceProvider;
		}		

		[Fact]
		public void CreatePrice()
		{
			var service = _serviceProvider.GetService<IPriceAppService>();

			PriceDTO price = new PriceDTO()
			{
				TukangId= 1,
				ProductId= 1,
				Size= 20,
				Price= 12000000
			};

			var result = service.Create(price);
			Assert.NotNull(result);
		}
	}
}
