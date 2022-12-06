//using AutoMapper;
//using FindTheBuilder.Applications.Services.CustomerAppServices.DTO;
//using FindTheBuilder.Databases;
//using FindTheBuilder.Databases.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FindTheBuilder.Applications.Services.CustomerAppServices
//{
//	public class CustomerAppService : ICustomerAppService
//	{
//		private readonly AppDbContext _context;
//		private readonly IMapper _mapper;
//		public CustomerAppService(AppDbContext context, IMapper mapper)
//		{
//			_context= context;
//			_mapper= mapper;
//		}
//		public Customers Create(CustomerDTO model)
//		{
//			var customer = _mapper.Map<Customers>(model);
//			_context.Customers.Add(customer);
//			_context.SaveChanges();

//			return customer;
//		}

//		public Customers Update(UpdateCustomerDTO model)
//		{
//			var customer = _mapper.Map<Customers>(model);
//			_context.Customers.Update(customer);
//			_context.SaveChanges();

//			return customer;
//		}
//	}
//}
