using AutoMapper;
using FindTheBuilder.Applications.Services.SkillAppServices.Dto;
using FindTheBuilder.Databases;
using FindTheBuilder.Databases.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.SkillAppServices
{
	public class SkillAppService : ISkillAppService
	{
		private AppDbContext _context;
		public SkillAppService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<ICollection<SkillDto>> GetAllSkill()
		{
			List<SkillDto> skillList = new List<SkillDto>();
			var skills = await _context.Skills.ToListAsync();
			foreach (var skill in skills)
			{
				skillList.Add(new SkillDto() { Id = skill.Id, Name = skill.Name});
			}
			return await Task.Run(() => (skillList));
		}
	}
}
