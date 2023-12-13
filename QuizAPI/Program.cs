global using Microsoft.EntityFrameworkCore;
global using QuizAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using QuizAPI.CustomMiddlewares;
using QuizAPI.Services;
using System.Net.Http.Headers;
using System.Text.Json;
//using WebApplication16.Data;


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlite("Data Source=questionDb.db"));
//builder.Services.AddDefaultIdentity<IdentityUser>(options =>
//                                 options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAuthentication("cookie").AddGoogle(
    options =>
    {
        options.SignInScheme = "cookie";
        options.ClientId = configuration["Authentication:Google:ClientId"];
        options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
        options.SaveTokens = true;
        options.Events.OnCreatingTicket = async ctx =>
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, ctx.Options.UserInformationEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ctx.AccessToken);
            using var result = await ctx.Backchannel.SendAsync(request);
            var user = await request.Content.ReadFromJsonAsync<JsonElement>();
            ctx.RunClaimActions(user);
        };
    }
    );




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<GlobalExceptionHandler>();

app.Run();
