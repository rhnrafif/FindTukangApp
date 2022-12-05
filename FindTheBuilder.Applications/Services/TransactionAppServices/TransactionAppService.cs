using AutoMapper;
using FindTheBuilder.Applications.Services.TransactionAppServices.DTO;
using FindTheBuilder.Databases;
using FindTheBuilder.Databases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.TransactionAppServices
{
	public class TransactionAppService : ITransactionAppService
	{
		private AppDbContext _contex;
		private IMapper _mapper;

		public TransactionAppService(AppDbContext contex, IMapper mapper)
		{
			_contex = contex;
			_mapper = mapper;
		}

		public Transactions Create(TransactionDTO model)
		{
			var trans = _mapper.Map<Transactions>(model);
			_contex.Transactions.Add(trans);
			_contex.SaveChanges();

			return trans;
		}

		public Transactions Update(UpdateTransactionDTO model)
		{
			var trans = _mapper.Map<Transactions>(model);
			_contex.Transactions.Update(trans);
			_contex.SaveChanges();

			return trans;
		}
	}
}
