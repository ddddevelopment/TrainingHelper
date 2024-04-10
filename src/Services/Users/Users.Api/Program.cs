using Users.Application.Services;
using Users.DAL;
using Users.DAL.Repositories;
using Users.Domain.Abstractions.Repositories;
using Users.Domain.Abstractions.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

var dbConfiguration = builder.Configuration.GetSection("DbConfiguration");
builder.Services.AddSingleton(config => new DbConfiguration(dbConfiguration.GetSection("Host").Value,
    dbConfiguration.GetSection("Database").Value, 
    dbConfiguration.GetSection("Username").Value,
    dbConfiguration.GetSection("Password").Value));

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
