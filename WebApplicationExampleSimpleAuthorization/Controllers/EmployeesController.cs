﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplicationExampleSimpleAuthorization.Models;

namespace WebApplicationExampleSimpleAuthorization.Controllers;

[Route("employees")]
[Authorize(Roles = "EMPLOYEE")] // Если ролей несколько, то они просто через запятую перечисляются
public class EmployeesController : Controller
{
    // Вместо БД
    private List<Employee> _employees = [
        new() { Id = Guid.Empty, Login = "login1", PasswordHash = "pass1" },
        new() { Login = "login2", PasswordHash = "pass2" }];

    [HttpGet("")]
    public string GetLogin()
    {
        var claimId = User.FindFirst("USER_ID")
            ?? throw new Exception();
        if (!Guid.TryParse(claimId.Value, out Guid userId))
            throw new Exception();

        var claimRole = User.FindFirst(ClaimsIdentity.DefaultRoleClaimType)
            ?? throw new Exception();
        var role = claimRole.Value;
        var employ = _employees.FirstOrDefault(x => x.Id == userId)
            ?? throw new Exception("Not found");

        return employ.Login;
    }
}
