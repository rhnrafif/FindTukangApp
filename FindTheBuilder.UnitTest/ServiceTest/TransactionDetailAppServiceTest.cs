using FindTheBuilder.Applications.Helper;
using FindTheBuilder.Applications.Services.TransactionDetailAppServices;
using FindTheBuilder.Applications.Services.TransactionDetailAppServices.DTO;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.UnitTest.ServiceTest
{
	public class TransactionDetailAppServiceTest : IClassFixture<Startup>
	{
		private ServiceProvider _serviceProvider;
		public TransactionDetailAppServiceTest(Startup fixtur)
		{
			_serviceProvider = fixtur.ServiceProvider;
		}

		[Fact]
		public void Create()
		{
			var service = _serviceProvider.GetService<ITransactionDetailAppService>();

			CreateTransactionDetailDTO trans = new CreateTransactionDetailDTO()
			{
				BuildingDay = 30,
				TransactionId = 1,
				ProductName= "Fountain"
			};

			var result = service.CreateTransactionDetail(trans);
			Assert.NotNull(result);
		}

		[Fact]
		public void GetAllTransactionDetail()
		{
			var service = _serviceProvider.GetService<ITransactionDetailAppService>();

			PageInfo page = new PageInfo()
			{
				Page = 1,
				PageSize = 5
			};

			var result = service.GetAllTransactions(page);
			Assert.NotNull(result);
		}
	}
}
