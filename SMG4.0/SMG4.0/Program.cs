using DBAccess.DBAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SMG4._0.Helpers;
using SMG4._0.Services;
using Serilog;
using FluentValidation.AspNetCore;
using FluentValidation;
using SMG4._0.Helpers.Validators;
using AutoMapper;
using SMG4._0.Mapping;
using SMG4._0.Repositories;
using System.Globalization;

namespace SMG4._0
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DiplomaProject", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AppSecret.AuthSecret.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = AppSecret.AuthSecret.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = AppSecret.AuthSecret.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

            builder.Services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<ITokenRepository, TokenRepository>();
            builder.Services.AddScoped<IEmployeeProfileService, EmployeeProfileService>();
            builder.Services.AddScoped<IEmployeeProfileRepository, EmployeeProfileRepository>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<AuthenticationRequestModelValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<ActivateEmployeeRequestModelValidator>();

            builder.Host.UseSerilog();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
