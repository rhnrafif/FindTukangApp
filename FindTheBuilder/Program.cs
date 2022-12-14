using FindTheBuilder.Applications.ConfigProfile;
using FindTheBuilder.Applications.Services.AuthAppServices;
using FindTheBuilder.Applications.Services.CustomerAppServices;
using FindTheBuilder.Applications.Services.PaymentAppServices;
using FindTheBuilder.Applications.Services.PriceAppServices;
using FindTheBuilder.Applications.Services.SkillAppServices;
using FindTheBuilder.Applications.Services.TransactionAppServices;
using FindTheBuilder.Applications.Services.TransactionDetailAppServices;
using FindTheBuilder.Applications.Services.TukangAppServices;
using FindTheBuilder.Databases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// DbContext
var connection = builder.Configuration.GetConnectionString("DBConnection");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connection));

//Add JWT Token Service -- install nugget Microsoft JWTBearer 6.0.8
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Issuer"],
			IssuerSigningKey = new SymmetricSecurityKey
			(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
		};
	});


var config = new AutoMapper.MapperConfiguration(cfg =>
{
	cfg.AddProfile(new ConfigurationProfile());
});

var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.
builder.Services.AddTransient<IAuthAppService, AuthAppService>();
builder.Services.AddTransient<ITukangAppService, TukangAppService>();
builder.Services.AddTransient<ICustomerAppService, CustomerAppService>();
builder.Services.AddTransient<ITransactionAppService, TransactionAppService>();
builder.Services.AddTransient<IPriceAppService, PriceAppService>();
builder.Services.AddTransient<ITransactionDetailAppService, TransactionDetailAppService>();
builder.Services.AddTransient<IPaymentAppService, PaymentAppService>();
builder.Services.AddTransient<ISkillAppService, SkillAppService>();

builder.Services.AddControllers();
builder.Services.AddControllersWithViews()
	.AddNewtonsoftJson(option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(cfg =>
{
	cfg.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "FindBuilder",
		Version = "v1"
	});
	cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		In = ParameterLocation.Header,
		Description = "Using JWT to acces API"
	});
	cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[]{}
		}
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
