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
		Task<bool> Create(CustomerDTO model);
		Task<bool> Update(UpdateCustomerDTO model);
		Task<Customers> GetByName (string name);
		Task<Customers> GetById(int id);
	}
}
