using Results.Api;
using Results.Application.Services;
using Results.DAL.Repositories;
using Results.Domain.Abstractions.Repositories;
using Results.Domain.Abstractions.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(ApiMappingProfile));

builder.Services.AddScoped<IResultsService, ResultsService>();
builder.Services.AddScoped<IResultsRepository, ResultsRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
