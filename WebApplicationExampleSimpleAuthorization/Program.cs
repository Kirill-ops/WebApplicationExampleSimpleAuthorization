using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "MyAuthServer", // ��� ��� ������� � ��������� ����� � �������� ��������

        ValidateAudience = true,
        ValidAudience = "MyAuthClient", // ��� ��� ������� � ��������� ����� � �������� ��������

        ValidateLifetime = true,

        // ��� ��� ������� � ��������� �����, ��������� ���� ������ ���� �������, 256 ��������. �������:
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
