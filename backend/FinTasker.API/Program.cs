using FinTasker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FinTasker.Application.Common.Interfaces;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Common.Interfaces.Repository;
using MediatR;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using FinTasker.Infrastructure.Services;
using FinTasker.Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using FinTasker.Application.Common.Behaviours;
using FluentValidation;
using FinTasker.API.Middleware;
using FinTasker.Application;


var builder = WebApplication.CreateBuilder(args);


// LOAD CONFIG DI AWAL 
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


builder.Services.AddHttpContextAccessor();

// AUTHENTICATION
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; // tetap untuk Google
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Cookies["access_token"];
            if (!string.IsNullOrEmpty(token))
                context.Token = token;
            return Task.CompletedTask;
        },
        OnChallenge = async context =>
        {
            context.HandleResponse();

            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                success = false,
                message = "User is not authenticated.",
                statusCode = 401,
                traceId = context.HttpContext.TraceIdentifier
            });
        },
        OnForbidden = async context =>
        {
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                success = false,
                message = "You do not have permission.",
                statusCode = 403,
                traceId = context.HttpContext.TraceIdentifier
            });
        },
    };
})
.AddCookie()
.AddGoogle(options =>
{
    var googleAuth = builder.Configuration.GetSection("Authentication:Google");
    options.ClientId = googleAuth["ClientId"]!;
    options.ClientSecret = googleAuth["ClientSecret"]!;
});


// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


// SERVICES

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//  DATABASE
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAppDbContext>(provider =>
    provider.GetRequiredService<AppDbContext>());


//  MEDIATR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.Load("FinTasker.Application"));  
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(
    typeof(ApplicationAssemblyMarker).Assembly);



//  SERVICES
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<ICookieService, CookieService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IProjectsService, ProjectsService>();
builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();

builder.Services.AddScoped<ITasksRepository, TasksRepository>();
builder.Services.AddScoped<ITasksService, TasksService>();

var app = builder.Build();

//  MIDDLEWARE
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // Nonaktifkan untuk development agar tidak perlu setup HTTPS
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<CookieToAuthorizationMiddleware>(); 
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();