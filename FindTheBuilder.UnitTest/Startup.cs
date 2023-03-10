using FindTheBuilder.Applications.ConfigProfile;
using FindTheBuilder.Applications.Services.CustomerAppServices;
using FindTheBuilder.Applications.Services.PriceAppServices;
using FindTheBuilder.Applications.Services.TransactionAppServices;
using FindTheBuilder.Applications.Services.TransactionDetailAppServices;
using FindTheBuilder.Applications.Services.TukangAppServices;
using FindTheBuilder.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.UnitTest
{
	public class Startup
	{
		public Startup()
		{
			var service = new ServiceCollection();
			//service.AddDbContext<AppDbContext>(opt =>
			//	opt.UseInMemoryDatabase
			//	("Server = Mad\\SQLEXPRESS; Database = REST_API; Trusted_Connection = True; TrustServerCertificate = True; "));

			service.AddDbContext<AppDbContext>(opt =>
				opt.UseInMemoryDatabase
				("Server=RHNRAFIF\\SQLEXPRESS;Database=TukangDB;Trusted_Connection=True;TrustServerCertificate=True;"));
			var config = new AutoMapper.MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new ConfigurationProfile());
			});

			var mapper = config.CreateMapper();

			service.AddSingleton(mapper);
			service.AddTransient<ICustomerAppService, CustomerAppService>();
			service.AddTransient<ITukangAppService, TukangAppService>();
			service.AddTransient<IPriceAppService, PriceAppService>();
			service.AddTransient<ITransactionAppService, TransactionAppService>();
			service.AddTransient<ITransactionDetailAppService, TransactionDetailAppService>();
			ServiceProvider = service.BuildServiceProvider();
		}

		public ServiceProvider ServiceProvider { get; private set; }
	}
}
