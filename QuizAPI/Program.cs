global using Microsoft.EntityFrameworkCore;
global using QuizAPI.Data;
using Microsoft.Extensions.DependencyInjection;
using QuizAPI.CustomMiddlewares;
using QuizAPI.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IQuestionService, QuestionService>();

builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlite("Data Source=questionDb.db"));

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
app.UseMiddleware<GlobalExceptionHandler>();

app.Run();
