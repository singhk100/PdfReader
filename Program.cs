using Microsoft.Extensions.DependencyInjection.Extensions;
using PdfReader;
using PdfReader.Interface;
using PdfReader.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var devCorsPolicy = "devCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy, builder => {

        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IService, Services>();
builder.Services.TryAddScoped<JsonConverts>();
builder.Services.TryAddScoped<Integration>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(devCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
