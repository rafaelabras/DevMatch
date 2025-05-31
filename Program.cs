using DevMatch.Data;
using DevMatch.Dtos.MessageDto;
using DevMatch.Helpers;
using DevMatch.Hubs;
using DevMatch.Interfaces;
using DevMatch.Models;
using DevMatch.Repository;
using DevMatch.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers().AddNewtonsoftJson(settings =>
{
    settings.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IMentorRepository, MentorRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddSingleton<IUserIdProvider, NameIdentifierUsedIdProvider>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageSevice>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IMentorService, MentorService>();

builder.Services.AddAuthentication(options =>
{
   options.DefaultAuthenticateScheme =
   options.DefaultChallengeScheme =
   options.DefaultForbidScheme =
   options.DefaultScheme =
   options.DefaultSignInScheme =
   options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(op =>
{
    var configuration = builder.Configuration;


    op.TokenValidationParameters = TokenHelpers.GetTokenValidationParameters(configuration);

    op.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var acessToken = context.Request.Query["acess_token"];
            var path = context.HttpContext.Request.Path;

            if (!string.IsNullOrEmpty(acessToken) && path.StartsWithSegments("/session-chat-hub"))
            {
                context.Token = acessToken;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "DevMatch API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Insira o token JWT no campo abaixo. Exemplo: Bearer {seu_token_aqui}"
    });


    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
             new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }

        }, new string[] {}

        } 

    });

});


builder.Services.AddSignalR().AddHubOptions<ChatHubs>(options =>

options.EnableDetailedErrors = true
);



builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddSignInManager<SignInManager<User>>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHub<ChatHubs>("session-chat-hub");
app.MapPost("enviarMensagem", EnviarMensagem);
async Task<IResult> EnviarMensagem(ConnectionDto message, [FromServices] IHubContext<ChatHubs> context)
{
    await context.Clients.All.SendAsync("Receive", message.Message);
    return Results.NoContent();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
