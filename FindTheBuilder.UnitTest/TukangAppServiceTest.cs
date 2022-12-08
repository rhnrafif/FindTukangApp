using FindTheBuilder.Applications.Services.TukangAppServices;
using FindTheBuilder.Applications.Services.TukangAppServices.DTO;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.UnitTest
{
	public class TukangAppServiceTest: IClassFixture<Startup>
	{
		private ServiceProvider _serviceProvider;
		public TukangAppServiceTest(Startup fixtur)
		{
			_serviceProvider = fixtur.ServiceProvider;
		}

		[Fact]
		public void CreateTukang()
		{
			var service = _serviceProvider.GetService<ITukangAppService>();

			TukangDTO tukang = new TukangDTO()
			{
				Name = "Test",
				SkillId = 1,
			};

			var result = service.Create(tukang);

			Assert.NotNull(result);
		}
		
		[Fact]
		public void UpdateTukang()
		{
			var service = _serviceProvider.GetService<ITukangAppService>();

			UpdateTukangDTO update = new UpdateTukangDTO()
			{
				Id = 1,
				Name = "Test",
				SkillId = 2,
			};

			var result = service.Update(update);

			Assert.NotNull(result);
		}
	}
}
