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
		public async Task<bool> Create(CustomerDTO model)
		{
			try
			{
				var customer = _mapper.Map<Customers>(model);
				await _context.Customers.AddAsync(customer);
				await _context.SaveChangesAsync();

				return await Task.Run(() => (true));
			}
			catch (Exception ex)
			{
				return await Task.Run(() => (false));
			}
		}

		public async Task<Customers> GetByName(string name)
		{
			var customer = new Customers();
			var cust = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(w => w.Name == name);
			if (cust == null)
			{
				return await Task.Run(() => (customer));
			}
			return await Task.Run(() => (customer = cust));
		}

		public async Task<bool> Update(UpdateCustomerDTO model)
		{
			try
			{
				var cust = await GetByName(model.Name);

				if (cust.Id != 0)
				{
					var customer = _mapper.Map<Customers>(model);
					customer.Id = cust.Id;
					_context.Customers.Update(customer);
					await _context.SaveChangesAsync();

					return await Task.Run(() => (true));
				}
				return await Task.Run(() => (false));
			}
			catch (Exception ex)
			{
				return await Task.Run(() => (false));
			}
		}

		public async Task<Customers> GetById(int id)
		{
			var customer = new Customers();
			var cust = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(w => w.Id == id);
			if (cust == null)
			{
				return await Task.Run(() => (customer));
			}
			return await Task.Run(() => (customer = cust));
		}
	}
}