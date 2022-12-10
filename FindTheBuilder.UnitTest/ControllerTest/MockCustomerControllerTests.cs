using FindTheBuilder.Applications.Helper;
using FindTheBuilder.Applications.Services.CustomerAppServices;
using FindTheBuilder.Applications.Services.CustomerAppServices.DTO;
using FindTheBuilder.Applications.Services.PriceAppServices;
using FindTheBuilder.Applications.Services.TransactionAppServices;
using FindTheBuilder.Applications.Services.TransactionAppServices.DTO;
using FindTheBuilder.Applications.Services.TransactionDetailAppServices;
using FindTheBuilder.Applications.Services.TransactionDetailAppServices.DTO;
using FindTheBuilder.Controllers;
using FindTheBuilder.Databases.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.UnitTest.ControllerTest
{
	public class MockCustomerControllerTests
	{
		private CustomerController _customerController;
		private Mock<ICustomerAppService> _customerAppService;
		private Mock<ITransactionAppService> _transactionAppService;
		private Mock<ITransactionDetailAppService> _transactionDetailAppService;
		private Mock<IPriceAppService> _priceAppService;
		public MockCustomerControllerTests()
		{
			_customerAppService = new Mock<ICustomerAppService>();
			_transactionAppService= new Mock<ITransactionAppService>();
			_transactionDetailAppService= new Mock<ITransactionDetailAppService>();
			_priceAppService= new Mock<IPriceAppService>();
			_customerController = new CustomerController(_customerAppService.Object,_transactionAppService.Object,_transactionDetailAppService.Object, _priceAppService.Object);
		}

		[Fact]
		public void CreateCustomerTestSuccess()
		{
			//Arrange
			CustomerDTO model = new CustomerDTO()
			{
				Name = "Mahdi",
				Address = "Magelang",
				Phone = "089897655677"
			};

			//Act
			var result = _customerController.CreateCustomer(model) as ObjectResult;

			//Assert
			Assert.Equal(200, result.StatusCode);
		}

		[Fact]
		public void CreateCustomerTestFailed()
		{
			//Arrange
			CustomerDTO model = new CustomerDTO();

			//Act
			var result = _customerController.CreateCustomer(model) as ObjectResult;

			//Assert
			Assert.Equal(400, result.StatusCode);
		}

		[Fact]
		public void UpdateCustomerTestSuccess()
		{
			//Arrange
			UpdateCustomerDTO model = new UpdateCustomerDTO()
			{
				Name = "Mahdi",
				Address = "Magelang",
				Phone = "089897655677"
			};

			//Act
			var result = _customerController.UpdateCustomer(model) as ObjectResult;

			//Assert
			Assert.NotNull(result);
		}


		[Fact]
		public void UpdateCustomerTestNameFailed()
		{			
			//Arrange
			UpdateCustomerDTO model = new UpdateCustomerDTO();			

			//Act
			var result = _customerController.UpdateCustomer(model) as ObjectResult;

			//Assert
			Assert.Equal(400, result.StatusCode);
		}

		[Fact]
		public void CreateTransactionSucces()
		{
			//Arrange
			TransactionDTO model = new TransactionDTO()
			{
				CustomerName = "Rafif",
				PriceId = 1
			};

			//Act
			var result = _customerController.CreateTransaction(model) as ObjectResult;

			//Assert
			Assert.NotNull(result);			
		}

		[Fact]
		public void UpdateTransactionSucces()
		{
			UpdateTransactionDTO model = new UpdateTransactionDTO()
			{				
				PriceId = 1
			};

			//Act
			var result = _customerController.UpdateTransaction(model) as ObjectResult;

			//Assert
			Assert.NotNull(result);
		}

		[Fact]
		public void GetAllTransactionSucces()
		{
			PageInfo page = new PageInfo()
			{
				Page = 1,
				PageSize = 10
			};

			//Act
			var result = _customerController.GetAllTransaction(page);

			//Assert
			Assert.NotNull(result);
		}

		[Fact]
		public void GetAllActiveTrasactionTest()
		{
			string name = "faisal";

			//Act
			var result = _customerController.GetActiveTransactionByName(name);

			//Assert
			Assert.NotNull(result);
		}

		[Fact]
		public void CreateTransactionDetailTest()
		{
			//Arrange
			CreateTransactionDetailDTO model = new CreateTransactionDetailDTO();

			//Act
			var result = _customerController.CreateTransDetail(model);

			//Assert
			Assert.NotNull(result);
		}

		[Fact]
		public void GetAllPriceTest()
		{
			PageInfo page = new PageInfo()
			{
				Page = 1,
				PageSize = 10
			};

			//Act
			var result = _customerController.GetAllPrice(page);

			//Assert
			Assert.NotNull(result);
		}
	}
}
