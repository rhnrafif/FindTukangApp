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
		Task<Prices> Create(PriceDTO model);
		Task<Prices> Update(UpdatePriceDTO model);
		Task<Prices> Delete(string product);
		Task<PagedResult<AllPriceListDTO>> GetAllPrice(PageInfo pageInfo);
		Task<Prices> GetPriceById(int id);
		Task<Prices> GetByProduct(string product);
		Task SaveImage(PriceDTO model);
		Task<Prices> DownloadImage(int id);
	}
}
