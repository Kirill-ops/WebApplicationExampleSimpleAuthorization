using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "MyAuthServer", // Вот это вынести в отдельный класс в качестве свойства

        ValidateAudience = true,
        ValidAudience = "MyAuthClient", // Вот это вынести в отдельный класс в качестве свойства

        ValidateLifetime = true,

        // Вот это вынести в отдельный класс, секретный ключ должен быть длинным, 256 символов. Напимер:
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("mysupersecret_secretkey!123123123123123")), 

        ValidateIssuerSigningKey = true,
    };
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
