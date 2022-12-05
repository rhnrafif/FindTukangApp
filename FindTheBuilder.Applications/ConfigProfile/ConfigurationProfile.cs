using AutoMapper;
using FindTheBuilder.Applications.Services.AuthAppServices.Dto;
//using FindTheBuilder.Applications.Services.CustomerAppServices.DTO;
using FindTheBuilder.Databases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.ConfigProfile
{
	public class ConfigurationProfile: Profile
	{
		public ConfigurationProfile()
		{
			// Customers
			//CreateMap<Customers, CustomerDTO>();
			//CreateMap<CustomerDTO ,Customers>();

			//CreateMap<Customers, UpdateCustomerDTO>();
			//CreateMap<UpdateCustomerDTO, Customers>();

			// Tukang

			//Auth
			CreateMap<Auth, AuthDto>();
			CreateMap<AuthDto, Auth>();
		}
	}
}
