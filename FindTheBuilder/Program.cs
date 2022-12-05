using FindTheBuilder.Applications.ConfigProfile;
<<<<<<< HEAD
=======
using FindTheBuilder.Applications.Services.AuthAppServices;
>>>>>>> Rafif
using FindTheBuilder.Databases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext
var connection = builder.Configuration.GetConnectionString("DBConnection");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connection));

var config = new AutoMapper.MapperConfiguration(cfg =>
{
	cfg.AddProfile(new ConfigurationProfile());
});

<<<<<<< HEAD
=======
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

>>>>>>> Rafif
// Add services to the container.
builder.Services.AddTransient<IAuthAppService, AuthAppService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
