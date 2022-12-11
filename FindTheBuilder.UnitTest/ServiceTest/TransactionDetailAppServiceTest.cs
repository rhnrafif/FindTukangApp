using FindTheBuilder.Applications.Helper;
using FindTheBuilder.Applications.Services.PriceAppServices;
using FindTheBuilder.Applications.Services.TransactionDetailAppServices;
using FindTheBuilder.Applications.Services.TransactionDetailAppServices.DTO;
using FindTheBuilder.Databases.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;
using Moq;
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
			var service = new Mock<ITransactionDetailAppService>();
			var priceService = new Mock<IPriceAppService>();

			//Arrange
			CreateTransactionDetailDTO trans = new CreateTransactionDetailDTO()
			{
				BuildingDay = 30,
				TransactionId = 1,
				PriceId = 1
			};
			Prices prices = new Prices()
			{
				Price = 1000
			};
			TransactionDetails transactionDetails = new TransactionDetails()
			{
				Id = 1
			};

			//Act
			var getRes = priceService.Setup(w => w.GetPriceById(trans.PriceId)).Returns(prices);
			var result = service.Setup(w => w.CreateTransactionDetail(trans)).Returns(transactionDetails);

			//Assert
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
