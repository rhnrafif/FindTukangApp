using FindTheBuilder.Applications.Services.CustomerAppServices.DTO;
using FindTheBuilder.Databases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.CustomerAppServices
{
	public interface ICustomerAppService
	{
		Customers Create(CustomerDTO model);
		Customers Update(UpdateCustomerDTO model);
	}
}
