﻿using AutoMapper;
using FindTheBuilder.Applications.Services.ProductAppServices.DTO;
using FindTheBuilder.Databases;
using FindTheBuilder.Databases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.ProductAppServices
{
	public class ProductAppService : IProductAppService
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		public ProductAppService(AppDbContext context, IMapper mapper)
		{
			_context= context;
			_mapper= mapper;
		}

		public Products Create(ProductDTO model)
		{
			var product = _mapper.Map<Products>(model);
			_context.Products.Add(product);
			_context.SaveChanges();

			return product;
		}

		public Products Delete(int id)
		{
			var product = _context.Products.FirstOrDefault(p => p.Id == id);
			if(product != null)
			{
				_context.Products.Remove(product);
				_context.SaveChanges();
			}
			
			return product;
		}

		public Products Update(UpdateProductDTO model)
		{
			var product = _mapper.Map<Products>(model);
			_context.Products.Update(product);
			_context.SaveChanges();

			return product;
		}
	}
}