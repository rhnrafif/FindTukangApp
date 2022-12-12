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

		public async Task<Transactions> Create(TransactionDTO model)
		{
			try
			{
				var dataTrans = new Transactions();

				var custData = await _customerAppService.GetByName(model.CustomerName);
				var transDate = DateTime.Now;
				var paymentStat = false;
				var trans = _mapper.Map<Transactions>(model);

				if(custData.Id != 0 && trans.PriceId != 0)
				{
					dataTrans.CustomerId = custData.Id;
					dataTrans.PriceId = trans.PriceId;
					dataTrans.TransactionDate = transDate;
					dataTrans.PaymentStatus = paymentStat;

					await _contex.Transactions.AddAsync(dataTrans);
					await _contex.SaveChangesAsync();

					return await Task.Run(()=>(dataTrans));
				}
				return await Task.Run(()=>(dataTrans));
			}
			catch
			{
				return await Task.Run(()=>(new Transactions() { CustomerId = 0 }));
			}			
		}

		public async Task<Transactions> Update(UpdateTransactionDTO model)
		{
			var transData = await _contex.Transactions.AsNoTracking().FirstOrDefaultAsync(w => w.Id == model.Id);
			var custData = await _customerAppService.GetByName(model.CustomerName);

			if(transData != null)
			{
				var trans = _mapper.Map<Transactions>(model);
				trans.CustomerId = custData.Id;
				trans.TransactionDate = transData.TransactionDate;
				_contex.Transactions.Update(trans);
				await _contex.SaveChangesAsync();

				return await Task.Run(()=>(trans));
			}
			return await Task.Run(()=>(transData));
		}

		public async Task<Transactions> UpdatePayment(int transId)
		{
			var transaction = new Transactions();
			try
			{
				var transData = await _contex.Transactions.AsNoTracking().FirstOrDefaultAsync(w => w.Id == transId);

				if (transData.Id != 0)
				{
					var trans = _mapper.Map<Transactions>(transData);
					trans.PaymentStatus = true;

					_contex.Transactions.Update(trans);
					await _contex.SaveChangesAsync();

					return await Task.Run(()=>(transaction = trans));
				}
				return await Task.Run(() => (transaction));
			}
			catch
			{
				return await Task.Run(() => (transaction));
			}
		}

		public async Task<ICollection<Transactions>> GetTransActiveByName(string name)
		{
			ICollection<Transactions> transaction = new Collection<Transactions>();
			try
			{
				var customerData = await _customerAppService.GetByName(name);
				if (customerData.Name == null)
				{
					return await Task.Run(()=>(transaction));
				}

				var trans = await _contex.Transactions.Where(w => w.Customer.Id == customerData.Id).Where(w => w.PaymentStatus == false).ToListAsync();
				if (trans.Count() != 0)
				{
					foreach (var d in trans)
					{
						var customer = await _customerAppService.GetById(d.CustomerId);

						d.Customer = customer;

						transaction.Add(d);
					}
					return await Task.Run(()=>(transaction));
				}
				return await Task.Run(()=>(transaction));
			}
			catch
			{
				return await Task.Run(() => (transaction));
			}
		}
	}
}
