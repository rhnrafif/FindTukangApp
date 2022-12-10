using FindTheBuilder.Applications.Services.PriceAppServices;
using FindTheBuilder.Applications.Services.PriceAppServices.DTO;
using FindTheBuilder.Applications.Services.TukangAppServices;
using FindTheBuilder.Applications.Services.TukangAppServices.DTO;
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
	public class MockTukangControllerTest
	{
		private TukangController _tukangController;
		private Mock<ITukangAppService> _tukangAppService;
		private Mock<IPriceAppService> _priceAppService;
		public MockTukangControllerTest()
		{
			_priceAppService= new Mock<IPriceAppService>();
			_tukangAppService = new Mock<ITukangAppService>();
			_tukangController = new TukangController(_tukangAppService.Object, _priceAppService.Object);
		}


		[Fact]
		public void CreateTukangTest()
		{
			//Arrange
			TukangDTO model = new TukangDTO
			{
				Name = "faisal"
			};
			Tukang tukang = new Tukang
			{
				Id = 1,
				Name = "faisal"
			};

			//Act
			_tukangAppService.Setup(w => w.Create(model)).Returns(tukang);
			var result = _tukangController.CreateTukang(model) as ObjectResult;

			//Assert
			Assert.Equal(200, result.StatusCode);
		}

		[Fact]
		public void EditTukangTest()
		{
			//Arrange
			UpdateTukangDTO updateData = new UpdateTukangDTO()
			{
				Name = "malik",
				Id = 1
			};

			Tukang tukang = new Tukang
			{
				Id = 1,
				Name = "malik"
			};

			//Act
			_tukangAppService.Setup(w => w.Update(updateData)).Returns(tukang);
			var result = _tukangController.EditTukang(updateData) as ObjectResult;

			//Assert
			Assert.Equal(200, result.StatusCode);
		}

		[Fact]
		public void CreatePricingTest()
		{
			//Arrange
			PriceDTO price = new PriceDTO()
			{
				TukangId = 1
			};
			Prices prices = new Prices()
			{
				TukangId = 1
			};

			//Act
			_priceAppService.Setup(w => w.Create(price)).Returns(prices);
			var result = _tukangController.CreatePricing(price) as ObjectResult;

			//Assert
			Assert.Equal(200, result.StatusCode);
		}

		[Fact]
		public void EditPricingTest()
		{
			//Arrange
			UpdatePriceDTO update = new UpdatePriceDTO()
			{
				Id = 1
			};

			Prices prices = new Prices()
			{
				TukangId = 1
			};

			//Act
			_priceAppService.Setup(w => w.Update(update)).Returns(prices);
			var result = _tukangController.EditPricing(update) as ObjectResult;

			//Assert
			Assert.Equal(200, result.StatusCode);
		}

		[Fact]
		public void DeletePricingTest()
		{
			//Arrange
			string prodcutName = "Tahu";
			Prices prices = new Prices()
			{
				TukangId = 1
			};

			//Act
			_priceAppService.Setup( w => w.Delete(prodcutName)).Returns(prices);
			var result = _tukangController.DeletPricing(prodcutName) as ObjectResult;

			//Assert
			Assert.Equal(200, result.StatusCode);
		}

	}
}
