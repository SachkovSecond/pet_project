using Microsoft.EntityFrameworkCore;
using PetProject.DataBase;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Run();