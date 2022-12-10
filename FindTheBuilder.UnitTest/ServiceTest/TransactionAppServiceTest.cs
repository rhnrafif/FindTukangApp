using FindTheBuilder.Applications.Services.TransactionAppServices;
using FindTheBuilder.Applications.Services.TransactionAppServices.DTO;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.UnitTest.ServiceTest
{
    public class TransactionAppServiceTest : IClassFixture<Startup>
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
                CustomerName = "Agus Ketoprak",
                PriceId = 1
            };

            var result = service.Create(trans);
            Assert.NotNull(result);
        }

        [Fact]
        public void Update()
        {
            var service = _serviceProvider.GetService<ITransactionAppService>();

            UpdateTransactionDTO trans = new UpdateTransactionDTO()
            {
                Id = 1,

                PriceId = 1
            };

            var result = service.Update(trans);
            Assert.NotNull(result);
        }

        [Fact]
        public void UpdatePayment()
        {
            var service = _serviceProvider.GetService<ITransactionAppService>();

            int id = 1;

            var result = service.UpdatePayment(id);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetActiveTransactionByName()
        {
            var service = _serviceProvider.GetService<ITransactionAppService>();

            string name = "Agus Ketoprak";

            var result = service.GetTransActiveByName(name);
            Assert.NotNull(result);
        }
    }
}
