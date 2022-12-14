using FindTheBuilder.Applications.Services.SkillAppServices.Dto;
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
		Task<ICollection<SkillDto>> GetAllSkill();
	}
}
