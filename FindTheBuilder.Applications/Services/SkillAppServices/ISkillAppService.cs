using FindTheBuilder.Applications.Services.SkillAppServices.DTO;
using FindTheBuilder.Databases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.SkillAppServices
{
	public interface ISkillAppService
	{
		Skills Create(SkillDTO model);
		Skills Update(UpdateSkillDTO model);
		Skills Delete (int id);
	}
}
