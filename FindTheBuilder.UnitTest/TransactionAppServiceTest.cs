using FindTheBuilder.Applications.Services.TransactionAppServices;
using FindTheBuilder.Applications.Services.TransactionAppServices.DTO;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.UnitTest
{
	public class TransactionAppServiceTest: IClassFixture<Startup>
	{
		private ServiceProvider _serviceProvider;
		public TransactionAppServiceTest(Startup fixtur)
		{
			_serviceProvider = fixtur.ServiceProvider;
		}

		[Fact]
		public void Create()
		{
			var service = _serviceProvider.GetService<ITransactionAppService>();

			TransactionDTO trans = new TransactionDTO()
			{
				CustomerName= "Agus Ketoprak",
				PriceId= 1
			};

			var result = service.Create(trans);
			Assert.NotNull(result);
		}
	}
}
