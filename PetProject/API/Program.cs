using Infrastructure.DataBase;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.Use(async (context, next) => 
app.MapControllers();
app.Run();