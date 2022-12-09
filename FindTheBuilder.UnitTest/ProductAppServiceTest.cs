using FindTheBuilder.Applications.Services.ProductAppServices;
using FindTheBuilder.Applications.Services.ProductAppServices.DTO;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.UnitTest
{
	public class ProductAppServiceTest: IClassFixture<Startup>
	{
		private ServiceProvider _serviceProvider;
		public ProductAppServiceTest(Startup fixtur)
		{
			_serviceProvider = fixtur.ServiceProvider;
		}

		[Fact]
		public void CreateProduct()
		{
			var service = _serviceProvider.GetService<IProductAppService>();

			ProductDTO newProduct = new ProductDTO()
			{
				Type = "Modern House"
			};

			var result = service.Create(newProduct);
			Assert.NotNull(result);
		}

		[Fact]
		public void UpdateProduct()
		{
			var service = _serviceProvider.GetService<IProductAppService>();

			UpdateProductDTO product = new UpdateProductDTO()
			{
				Id= 1,
				Type = "Dog House"
			};

			var result = service.Update(product);
			Assert.NotNull(result);
		}

		[Fact]
		public void DeleteProduct()
		{
			var service = _serviceProvider.GetService<IProductAppService>();
			int id = 1;

			var result = service.Delete(id); 
			Assert.Null(result);
		}
	}
}
