using AutoMapper;
using FindTheBuilder.Applications.Services.CustomerAppServices;
using FindTheBuilder.Applications.Services.PriceAppServices;
using FindTheBuilder.Applications.Services.TransactionAppServices.DTO;
using FindTheBuilder.Databases;
using FindTheBuilder.Databases.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
		private readonly IPriceAppService _priceAppService;

		public TransactionAppService(AppDbContext contex, IMapper mapper, ICustomerAppService customerAppService, IPriceAppService priceAppService)
		{
			_contex = contex;
			_mapper = mapper;
			_customerAppService = customerAppService;
			_priceAppService = priceAppService;
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

		public Transactions UpdatePayment(int transId)
		{
			var transaction = new Transactions();
			try
			{
				var transData = _contex.Transactions.AsNoTracking().FirstOrDefault(w => w.Id == transId);

				if (transData.Id != 0)
				{
					var trans = _mapper.Map<Transactions>(transData);
					trans.PaymentStatus = true;

					_contex.Transactions.Update(trans);
					_contex.SaveChanges();

					return transaction = trans;
				}
				return transaction;
			}
			catch
			{
				return transaction;
			}
		}

		public ICollection<Transactions> GetTransActiveByName(string name)
		{
			ICollection<Transactions> transaction = new Collection<Transactions>();
			try
			{
				var customerData = _customerAppService.GetByName(name);
				if (customerData.Name == null)
				{
					return transaction;
				}

				var trans = _contex.Transactions.Where(w => w.Customer.Id == customerData.Id).Where(w => w.PaymentStatus == false).ToList();
				if (trans.Count() != 0)
				{
					foreach (var d in trans)
					{
						var customer = _customerAppService.GetById(d.CustomerId);

						d.Customer = customer;

						transaction.Add(d);
					}
					return transaction;
				}
				return transaction;
			}
			catch
			{
				return transaction;
			}
		}
	}
}
