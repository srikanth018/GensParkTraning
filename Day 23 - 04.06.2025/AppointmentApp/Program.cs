using System.Text.Json.Serialization;
using AppointmentApp.Contexts;
using AppointmentApp.Interfaces;
using AppointmentApp.Misc;
using AppointmentApp.Models;
using AppointmentApp.Repositories;
using AppointmentApp.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Logging;

IdentityModelEventSource.ShowPII = true;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Clinic API", Version = "v1" });

    opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://dev-de5ffoekrcokjysz.us.auth0.com/authorize"),
                TokenUrl = new Uri("https://dev-de5ffoekrcokjysz.us.auth0.com/oauth/token"),
                Scopes = new Dictionary<string, string>
            {
                { "read:messages", "Read access to messages" }
            }
            }
        }
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            new[] { "read:messages" }
        }
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Logging.AddLog4Net();

builder.Services.AddDbContext<ClinicContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Repositories
builder.Services.AddTransient<IRepository<int, Doctor>, DoctorRepository>();
builder.Services.AddTransient<IRepository<int, DoctorSpeciality>, DoctorSpecialityRepository>();
builder.Services.AddTransient<IRepository<int, Speciality>, SpecialityRepository>();
builder.Services.AddTransient<IRepository<string, User>, UserRepository>();
builder.Services.AddTransient<IRepository<int, Patient>, PatientRepository>();
builder.Services.AddTransient<IRepository<int, Appointment>, AppointmentRepository>();

// Services
builder.Services.AddTransient<IDoctorService, DoctorService>();
builder.Services.AddTransient<IDoctorSepcialityService, DoctorSpecilaityService>();
builder.Services.AddTransient<ISpecialityService, SpecialityService>();
builder.Services.AddTransient<IPatientService, PatientService>();
builder.Services.AddTransient<IAppointmentService, AppointmentService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IFileHandlingService, FileHandlingService>();

// Misc
builder.Services.AddTransient<IOtherContextFunctionities, OtherFuncinalitiesImplementation>();
builder.Services.AddTransient<UserPatientMapper>();
builder.Services.AddAutoMapper(typeof(User));
builder.Services.AddScoped<CustomExceptionFilters>();

// JWT Authentication using Auth0
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://dev-de5ffoekrcokjysz.us.auth0.com/";

        // Replace this with your actual Auth0 API Identifier from the dashboard (not localhost)
        options.Audience = "https://dev-de5ffoekrcokjysz.us.auth0.com/api/v2/";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://dev-de5ffoekrcokjysz.us.auth0.com/",
            ValidateAudience = true,
            ValidAudience = "https://dev-de5ffoekrcokjysz.us.auth0.com/api/v2/",
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token successfully validated.");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ExperiencedDoctorOnly", policy =>
        policy.Requirements.Add(new ExperiencedDoctorRequirement(3)));
});

builder.Services.AddSingleton<IAuthorizationHandler, ExperiencedDoctorHandler>();

var app = builder.Build();

// Development middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clinic API v1");

        c.OAuthClientId("j1jYsiNzHGCLdjb6jwpizRCDmuDX7xwJ");
        c.OAuthClientSecret("v8GeLWLS3z9sMAiDJrhzs79spsk8i1tnhsjL1JZUmBJBF9KDnsWUo12GJD2iaJXb");
        c.OAuthUsePkce();
        c.OAuthScopeSeparator(" ");
        c.OAuthScopes("read:messages");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
