using AutoMapper;
using FindTheBuilder.Applications.Services.CustomerAppServices.DTO;
using FindTheBuilder.Databases;
using FindTheBuilder.Databases.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.CustomerAppServices
{
	public class CustomerAppService : ICustomerAppService
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		public CustomerAppService(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public Customers Create(CustomerDTO model)
		{
			var customer = _mapper.Map<Customers>(model);
			_context.Customers.Add(customer);
			_context.SaveChanges();

			return customer;
		}

		public Customers GetByName(string name)
		{
			var customer = new Customers();
			var cust = _context.Customers.AsNoTracking().FirstOrDefault(w => w.Name == name);
			if(cust == null)
			{
				return customer;
			}
			return customer = cust;
		}

		public Customers Update(UpdateCustomerDTO model)
		{
			var cust = GetByName(model.Name);

			if(cust.Id != 0)
			{
				var customer = _mapper.Map<Customers>(model);
				customer.Id = cust.Id;
				_context.Customers.Update(customer);
				_context.SaveChanges();

				return customer;
			}
			return new Customers() { Name = null };
		}
	}
}
