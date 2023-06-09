using SchoolAdministration.Services;
using SchoolAdministration.Utils;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using SchoolAdministration.DTO;
using System.Configuration;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStaffService, StaffService>();
string connString = builder.Configuration.GetConnectionString("MyConnectionString");
builder.Services.AddTransient<DataAccess>(_ => new DataAccess(connString));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
//builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);
var app = builder.Build();
app.UseHttpsRedirection();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
/*app.Use(async (context, next) =>
{
    if (!context.User.Identity.IsAuthenticated)
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Not Authenticated");
    }
    else await next();

});*/
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
