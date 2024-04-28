using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
    options => options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidIssuer = "Issuer",
        ValidateAudience = false,
        ValidAudience = "Audience",
        ValidateLifetime = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret secret secret secret secret")),
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

app.Map("/login/{username}", (string username) =>
{
    var claims = new List<Claim>{ new Claim(ClaimTypes.Name, username) };
    var jwt = new JwtSecurityToken(
        issuer: "Issuer",
        audience: "Audience",
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(3), 
        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret secret secret secret secret")), SecurityAlgorithms.HmacSha256));

    return new JwtSecurityTokenHandler().WriteToken(jwt);
});

app.Run();
