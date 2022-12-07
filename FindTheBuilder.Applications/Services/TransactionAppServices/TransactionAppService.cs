using AutoMapper;
using FindTheBuilder.Applications.Services.CustomerAppServices;
using FindTheBuilder.Applications.Services.TransactionAppServices.DTO;
using FindTheBuilder.Databases;
using FindTheBuilder.Databases.Models;
using Microsoft.EntityFrameworkCore;
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
		private readonly ICustomerAppService _customerAppService;

		public TransactionAppService(AppDbContext contex, IMapper mapper, ICustomerAppService customerAppService)
		{
			_contex = contex;
			_mapper = mapper;
			_customerAppService = customerAppService;
		}

		public Transactions Create(TransactionDTO model)
		{
			try
			{
				var dataTrans = new Transactions();

				var custData = _customerAppService.GetByName(model.CustomerName);
				var transDate = DateTime.Now;
				var paymentStat = false;
				var trans = _mapper.Map<Transactions>(model);

				if(custData.Id != 0 && trans.PriceId != 0)
				{
					dataTrans.CustomerId = custData.Id;
					dataTrans.PriceId = trans.PriceId;
					dataTrans.TransactionDate = transDate;
					dataTrans.PaymentStatus = paymentStat;

					_contex.Transactions.Add(dataTrans);
					_contex.SaveChanges();

					return dataTrans = trans;
				}
				return dataTrans;
			}
			catch
			{
				return new Transactions() { CustomerId = 0 };
			}			
		}

		public Transactions Update(UpdateTransactionDTO model)
		{
			var transData = _contex.Transactions.AsNoTracking().FirstOrDefault(w => w.Id == model.Id);

			if(transData != null)
			{
				var trans = _mapper.Map<Transactions>(model);
				_contex.Transactions.Update(trans);
				_contex.SaveChanges();

				return trans;
			}
			return transData;
		}
	}
}
