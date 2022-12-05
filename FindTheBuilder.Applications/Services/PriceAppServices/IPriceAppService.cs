using FindTheBuilder.Applications.Helper;
using FindTheBuilder.Applications.Services.PriceAppServices.DTO;
using FindTheBuilder.Databases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.PriceAppServices
{
	public interface IPriceAppService
	{
		Prices Create(PriceDTO model);
		Prices Update(UpdatePriceDTO model);
		Prices Delete(int id);
		PagedResult<PriceListDTO> GetPriceByProduct(PageInfo pageInfo);
	}
}
