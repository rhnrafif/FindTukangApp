using FindTheBuilder.Applications.Services.TukangAppServices.DTO;
using FindTheBuilder.Databases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.TukangAppServices
{
	public interface ITukangAppService
	{
		Task<Tukang> Create(TukangDTO model);
		Task<Tukang> Update(UpdateTukangDTO model);
	}
}
