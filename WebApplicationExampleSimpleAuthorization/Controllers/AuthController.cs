using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplicationExampleSimpleAuthorization.Models;

namespace WebApplicationExampleSimpleAuthorization.Controllers;

[Route("auth")]
[AllowAnonymous]
public class AuthController : Controller
{
    // Вместо БД
    private readonly List<Employee> _employees = [
        new() { Id = Guid.Empty, Login = "login1", PasswordHash = "pass1" }, 
        new() { Login = "login2", PasswordHash = "pass2" }];

    // Просто другой класс не хочу создавать, так что будет такой же
    private readonly List<Employee> _admins = [
        new() { Id = Guid.Empty, Login = "admin", PasswordHash = "pass1" }];

    [HttpGet("employ")]
    public string AuthEmploy([FromQuery] string login, [FromQuery] string password)
    {
        var employee = _employees.FirstOrDefault(x => x.Login == login && x.PasswordHash == password); // TODO 1: вообще, конечно, да, надо сравнивать хеши, но для примера просто пароли

        if (employee is null)
            throw new Exception(); // лучше создать свое исключение с нормальным названием, типо NotAuthorizationException и бросать его

        var token = CreateToken(employee, "EMPLOYEE");

        return token;
    }

    [HttpGet("admin")]
    public string AuthAdmin([FromQuery] string login, [FromQuery] string password)
    {
        var employee = _employees.FirstOrDefault(x => x.Login == login && x.PasswordHash == password); // TODO 1: вообще, конечно, да, надо сравнивать хеши, но для примера просто пароли

        if (employee is null)
            throw new Exception(); // лучше создать свое исключение с нормальным названием, типо NotAuthorizationException и бросать его

        var token = CreateToken(employee, "ADMIN");

        return token;
    }

    private string CreateToken(Employee employee, string role)
    {
        var jwt = new JwtSecurityToken(
            issuer: "MyAuthServer",  // Вот это брать из отдельного класса 
            audience: "MyAuthClient", // Вот это брать из отдельного класса 
            claims: [
                new("USER_ID", employee.Id.ToString()),
                new(ClaimsIdentity.DefaultRoleClaimType, role)],
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes("mysupersecret_secretkey!123123123123123")), // Вот это брать из отдельного класса 
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
