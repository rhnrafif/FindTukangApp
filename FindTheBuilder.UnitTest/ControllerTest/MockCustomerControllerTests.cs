using FindTheBuilder.Applications.Helper;
using FindTheBuilder.Applications.Services.CustomerAppServices;
using FindTheBuilder.Applications.Services.CustomerAppServices.DTO;
using FindTheBuilder.Applications.Services.PriceAppServices;
using FindTheBuilder.Applications.Services.PriceAppServices.DTO;
using FindTheBuilder.Applications.Services.TransactionAppServices;
using FindTheBuilder.Applications.Services.TransactionAppServices.DTO;
using FindTheBuilder.Applications.Services.TransactionDetailAppServices;
using FindTheBuilder.Applications.Services.TransactionDetailAppServices.DTO;
using FindTheBuilder.Controllers;
using FindTheBuilder.Databases.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
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
			Task<bool> res = Task.Run(()=>(true));

			//Act
			_customerAppService.Setup(w => w.Create(model)).Returns(res);
			var result = _customerController.CreateCustomer(model).Result as ObjectResult;

			//Assert
			Assert.Equal(200, result.StatusCode);
		}

		[Fact]
		public void CreateCustomerTestFailed()
		{
			//Arrange
			CustomerDTO model = new CustomerDTO() { Name = ""};

			//Act
			var result = _customerController.CreateCustomer(model).Result as ObjectResult;

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
			Task<bool> res = Task.Run(() => (true));

			//Act
			_customerAppService.Setup(w => w.Update(model)).Returns(res);
			var result = _customerController.UpdateCustomer(model).Result as ObjectResult;

			//Assert
			Assert.Equal(200, result.StatusCode);
		}


		[Fact]
		public void UpdateCustomerTestNameFailed()
		{
			//Arrange
			UpdateCustomerDTO model = new UpdateCustomerDTO() { Name = ""};

			//Act
			var result = _customerController.UpdateCustomer(model).Result as ObjectResult;

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
			Task<Transactions> trans = Task.Run(()=>(new Transactions() { Id = 1}));

			//Act
			_transactionAppService.Setup(w => w.Create(model)).Returns(trans);
			var result = _customerController.CreateTransaction(model).Result as ObjectResult;

			//Assert
			Assert.Equal(200, result.StatusCode);
		}

		[Fact]
		public void UpdateTransactionSucces()
		{
			UpdateTransactionDTO model = new UpdateTransactionDTO()
			{
				CustomerName = "1",
				PriceId = 1
			};
			Task<Transactions> trans = Task.Run(()=>(new Transactions() { Id = 1 }));

			//Act
			_transactionAppService.Setup(w => w.Update(model)).Returns(trans);
			var result = _customerController.UpdateTransaction(model).Result as ObjectResult;

			//Assert
			Assert.Equal(200, result.StatusCode);
		}

		[Fact]
		public void GetAllTransactionSucces()
		{
			PageInfo page = new PageInfo()
			{
				Page = 1,
				PageSize = 10
			};

			var trans1 = new TransactionDetailDTO { Id = 1 };
			var trans2 = new TransactionDetailDTO { Id = 9 };

			var data = new List<TransactionDetailDTO>();
			data.Add(trans1);
			data.Add(trans2);

			IEnumerable<TransactionDetailDTO> dataTrans = data.AsEnumerable();
			PagedResult<TransactionDetailDTO> transactionList = new PagedResult<TransactionDetailDTO>();
			transactionList.Data = dataTrans;

			Task<PagedResult<TransactionDetailDTO>> transResult = Task.Run(()=>(transactionList));
			
			//Act
			_transactionDetailAppService.Setup(w => w.GetAllTransactions(page)).Returns(transResult);
			var result = _customerController.GetAllTransaction(page).Result as ObjectResult;

			//Assert
			Assert.Equal(200, result.StatusCode);
		}

		[Fact]
		public void GetAllActiveTrasactionTest()
		{
			//Arrange
			PageInfo page = new PageInfo()
			{
				Page = 1,
				PageSize = 10
			};
			var trans1 = new Transactions { CustomerId = 1 };
			var trans2 = new Transactions { CustomerId = 2 };

			ICollection<Transactions> data = new List<Transactions>();
			data.Add(trans1);
			data.Add(trans2);

			Task<ICollection<Transactions>> activeTrans = Task.Run(() => (data));
			

			int id = 1;

			//Act
			_transactionAppService.Setup(w => w.GetTransActiveById(id)).Returns(activeTrans);
			var result = _customerController.GetActiveTransactionById(id).Result as ObjectResult;

			//Assert
			Assert.Equal(200, result.StatusCode);
		}

		[Fact]
		public void CreateTransactionDetailTest()
		{
			//Arrange
			CreateTransactionDetailDTO model = new CreateTransactionDetailDTO()
			{
				BuildingDay = 2,
				PriceId = 1,
				TransactionId = 1
			};
			Task<TransactionDetails> mod = Task.Run(() => (new TransactionDetails() { Id = 1}));

			//Act
			_transactionDetailAppService.Setup(w => w.CreateTransactionDetail(model)).Returns(mod);
			var result = _customerController.CreateTransDetail(model).Result as ObjectResult;


			//Assert
			Assert.Equal(200, result.StatusCode);
		}

		[Fact]
		public void GetAllPriceTest()
		{
			PageInfo page = new PageInfo()
			{
				Page = 1,
				PageSize = 10
			};

			var detail1 = new AllPriceListDTO() { Price = 1 };
			var detail2 = new AllPriceListDTO() { Price = 2 };
			var detail = new List<AllPriceListDTO>();
			detail.Add(detail1);
			detail.Add(detail2);

			IEnumerable<AllPriceListDTO> priceList = detail.AsEnumerable();

			var data = new PagedResult<AllPriceListDTO>();
			data.Data = priceList;
			data.Total = 2;

			Task<PagedResult<AllPriceListDTO>> priceResult = Task.Run(() => (data));

			//Act
			_priceAppService.Setup(w => w.GetAllPrice(page)).Returns(priceResult);
			var result = _customerController.GetAllPrice(page).Result as ObjectResult;

			//Assert
			Assert.Equal(200, result.StatusCode);
		}
	}
}
