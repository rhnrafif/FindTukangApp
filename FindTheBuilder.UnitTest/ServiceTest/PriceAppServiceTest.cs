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
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using FindTheBuilder.Applications.Helper;
using Moq;
using FindTheBuilder.Databases.Models;
using Microsoft.AspNetCore.Mvc;

namespace FindTheBuilder.UnitTest.ServiceTest
{
    public class PriceAppServiceTest : IClassFixture<Startup>
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
                SkillId = 1,
                Product = "Your Mom",
                Size = 100,
                Price = 100000
            };

            var result = service.Create(price);
            Assert.NotNull(result);
        }

        [Fact]
        public void UpdatePrice()
        {
            var service = _serviceProvider.GetService<IPriceAppService>();

            UpdatePriceDTO price = new UpdatePriceDTO()
            {
                Id = 1,
                SkillId = 1,
                Product = "Your Mom is Gay",
                Size = 100,
                Price = 100000
            };

            var result = service.Update(price);
            Assert.NotNull(result);
        }

        [Fact]
        public void DeletePrice()
        {
            var service = new Mock<IPriceAppService>();

            string product = "Your Mom is Gay";
            Task<Prices> price = Task.Run(() => (new Prices() { Id = 1}));

            var result = service.Setup(w => w.Delete(product)).Returns(price);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllPrices()
        {
            var service = _serviceProvider.GetService<IPriceAppService>();

            PageInfo page = new PageInfo()
            {
                Page = 1,
                PageSize = 5
            };

            //var result = service.GetPriceByProduct(page);
            var result = service.GetAllPrice(page);
            Assert.NotNull(result);
        }
    }
}
