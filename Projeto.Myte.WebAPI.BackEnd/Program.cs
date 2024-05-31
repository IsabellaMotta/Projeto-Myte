using Microsoft.EntityFrameworkCore;
using Projeto.Myte.WebAPI.BackEnd.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Refer�ncia do DbContext configurado na pasta models para iniciar o service que integra a API e o DB
builder.Services.AddDbContext<MyteDbContext>(
	options => { 
		options.UseSqlServer(builder.Configuration.GetConnectionString
		("DefaultConnection"));
	});

//adicionar o service para inicializa��o do cors
//cruzamento de dominio entre aplica��es
builder.Services.AddCors(
	options =>
	{
		options.AddPolicy("Cors", p =>
		{
			p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
		});
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
//Uso das politas de integra��o definidas acima. Isso autoriza o front a usar a aplica��o
app.UseCors("Cors");

app.UseAuthorization();

app.MapControllers();

app.Run();
