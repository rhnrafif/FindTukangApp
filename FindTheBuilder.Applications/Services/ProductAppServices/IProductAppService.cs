using FindTheBuilder.Applications.Services.ProductAppServices.DTO;
using FindTheBuilder.Databases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.ProductAppServices
{
	public interface IProductAppService
	{
		Products Create (ProductDTO model);
		Products Update (UpdateProductDTO model);
		Products Delete( int id);
	}
}
