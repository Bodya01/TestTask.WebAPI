using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TestTask.Data.Context;
using TestTask.Data.Dto;
using TestTask.Data.Entities;
using TestTask.Data.Infrastructure;
using TestTask.Domain.FluentValidation;
using TestTask.Domain.Services.Implementations;
using TestTask.Domain.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMvc().AddFluentValidation();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepository<Incident>, Repository<Incident>>();
builder.Services.AddScoped<IRepository<Account>, Repository<Account>>();
builder.Services.AddScoped<IRepository<Contact>, Repository<Contact>>();

builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IIncidentService, IncidentService>();

builder.Services.AddDbContext<TestTaskContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelsExpandDepth(-1);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
