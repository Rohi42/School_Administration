
using Microsoft.AspNetCore.Mvc;
using SchoolAdministration.Services;
using SchoolAdministration.Utils;
using SchoolAdministration.Controllers;
using MySql.Data.MySqlClient;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStaffService, StaffService>();
string connString = builder.Configuration.GetConnectionString("MyConnectionString");
builder.Services.AddTransient<DataAccess>(_ => new DataAccess(connString));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
