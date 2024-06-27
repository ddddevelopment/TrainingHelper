using Users.Api;
using Users.Application.Services;
using Users.DAL;
using Users.DAL.Repositories;
using Users.Domain.Abstractions.Repositories;
using Users.Domain.Abstractions.Services;
using Users.MessageBroker.RabbitMQ;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(ApiMappingProfile), typeof(DALMappingProfile));

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

var dbConfiguration = builder.Configuration.GetSection("DbConfiguration");
builder.Services.AddSingleton(config => new DbConfiguration(dbConfiguration.GetSection("Host").Value,
    dbConfiguration.GetSection("Database").Value,
    dbConfiguration.GetSection("Username").Value,
    dbConfiguration.GetSection("Password").Value));

IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();

var rpcServer = new RpcServer("users_request", serviceProvider.GetService<IUsersRepository>());

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
