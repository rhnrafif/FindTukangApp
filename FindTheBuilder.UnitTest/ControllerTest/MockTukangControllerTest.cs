using FindTheBuilder.Applications.Helper;
using FindTheBuilder.Applications.Services.PriceAppServices;
using FindTheBuilder.Applications.Services.PriceAppServices.DTO;
using FindTheBuilder.Applications.Services.SkillAppServices;
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
		private Mock<ISkillAppService> _skillAppService;
		public MockTukangControllerTest()
		{
			_priceAppService= new Mock<IPriceAppService>();
			_tukangAppService = new Mock<ITukangAppService>();
			_skillAppService = new Mock<ISkillAppService>();
			_tukangController = new TukangController(_tukangAppService.Object, _priceAppService.Object, _skillAppService.Object);
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

			Task<Tukang> tukangResult = Task.Run(() => (tukang));

			//Act
			_tukangAppService.Setup(w => w.Create(model)).Returns(tukangResult);
			var result = _tukangController.CreateTukang(model).Result as ObjectResult;

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

			Task<Tukang> tukangResult = Task.Run(() => (tukang));

			//Act
			_tukangAppService.Setup(w => w.Update(updateData)).Returns(tukangResult);
			var result = _tukangController.EditTukang(updateData).Result as ObjectResult;

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

			Task<Prices> priceResult = Task.Run(() => (prices));

			//Act
			_priceAppService.Setup(w => w.Create(price)).Returns(priceResult);
			var result = _tukangController.CreatePricing(price).Result as ObjectResult;

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
				Id = 1
			};

			Task<Prices> priceResult = Task.Run(() => (prices));

			//Act
			_priceAppService.Setup(w => w.Update(update)).Returns(priceResult);
			var result = _tukangController.EditPricing(update).Result as ObjectResult;

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

			Task<Prices> priceResult = Task.Run(()=>(prices));

			//Act
			_priceAppService.Setup(w => w.Delete(prodcutName)).Returns(priceResult);
			var result = _tukangController.DeletPricing(prodcutName).Result as ObjectResult;

			//Assert
			Assert.Equal(200, result.StatusCode);
		}

	}
}
