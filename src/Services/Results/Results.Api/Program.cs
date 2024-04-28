using FluentValidation;
using Results.Api;
using Results.Api.Models;
using Results.Application.Services;
using Results.DAL;
using Results.DAL.Repositories;
using Results.Domain.Abstractions.Repositories;
using Results.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(DALMappingProfile));

builder.Services.AddSingleton<IValidator<ResultRequest>, ResultPresentationValidator>();

var dbConfiguration = builder.Configuration.GetSection("DbConfiguration");

builder.Services.AddSingleton(configuration => new DbConfiguration(
                            dbConfiguration.GetSection("Host").Value,
                            dbConfiguration.GetSection("Database").Value,
                            dbConfiguration.GetSection("Username").Value,
                            dbConfiguration.GetSection("Password").Value));

builder.Services.AddScoped<IResultsService, ResultsService>();
builder.Services.AddScoped<IResultsRepository, ResultsRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidIssuer = AuthOptions.ISSUER,
        ValidateAudience = false,
        ValidAudience = AuthOptions.AUDIENCE,
        ValidateLifetime = false,
        IssuerSigningKey = AuthOptions.SymmetricKey,
        ValidateIssuerSigningKey = true
    });

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
