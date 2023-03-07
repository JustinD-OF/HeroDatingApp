using System.Text;
using HeroDatingApp.Data;
using HeroDatingApp.Extensions;
using HeroDatingApp.Interfaces;
using HeroDatingApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200")); 

// Use middleware between Cors usage and Controller mapping
app.UseAuthentication(); // Do you have valid token
app.UseAuthorization(); // what are you allowed to do

app.MapControllers();

app.Run();
