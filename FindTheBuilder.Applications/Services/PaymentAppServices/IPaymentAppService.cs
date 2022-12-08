using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.PaymentAppServices
{
	public interface IPaymentAppService
	{
		Task<object> Post();
	}
}
