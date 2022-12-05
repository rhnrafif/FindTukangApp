using AutoMapper;
using FindTheBuilder.Applications.Services.SkillAppServices.DTO;
using FindTheBuilder.Databases;
using FindTheBuilder.Databases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.SkillAppServices
{
	public class SkillAppService : ISkillAppService
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;

		public SkillAppService(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public Skills Create(SkillDTO model)
		{
			var skill = _mapper.Map<Skills>(model);
			_context.Skills.Add(skill);
			_context.SaveChanges();

			return skill;
		}

		public Skills Delete(int id)
		{
			var skill = _context.Skills.FirstOrDefault(x => x.Id == id);
			if (skill != null)
			{
				_context.Skills.Remove(skill);
				_context.SaveChanges();
			}

			return skill;
		}

		public Skills Update(UpdateSkillDTO model)
		{
			var skill = _mapper.Map<Skills>(model);
			_context.Skills.Update(skill);
			_context.SaveChanges();

			return skill;
		}
	}
}
