using System;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MMM_Bracket.API.Domain.Models.Configuration;
using MMM_Bracket.API.Domain.Repositories;
using MMM_Bracket.API.Domain.Services;
using MMM_Bracket.API.Mapping;
using MMM_Bracket.API.Persistence.Contexts;
using MMM_Bracket.API.Persistence.Repositories;
using MMM_Bracket.API.Services;
using Microsoft.AspNetCore.Hosting;

namespace MMM_Bracket.API
{
  public class Startup
  {

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
      
      services.AddAutoMapper(typeof(ModelToResourceProfile));

      services.AddDbContext<mmm_bracketContext>(options =>
          options.UseNpgsql(Configuration.GetSection("Database").GetValue<string>("ConnectionString")));

      services.AddSingleton(Configuration);
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

      services.AddScoped<IAnimalRepository, AnimalRepository>();
      services.AddScoped<IAnimalService, AnimalService>();

      services.AddScoped<ICategoryRepository, CategoryRepository>();
      services.AddScoped<ICategoryService, CategoryService>();

      services.AddScoped<IParticipantRepository, ParticipantRepository>();
      services.AddScoped<IParticipantService, ParticipantService>();

      services.AddScoped<IJWTTokenService, JWTTokenService>();

      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IUserService, UserService>();

      services.AddScoped<IResultsRepository, ResultsRepository>();
      services.AddScoped<IResultsService, ResultsService>();

      //configure JWTBearer Authentication
      var tokenSettings = Configuration.GetSection("JWTSettings").Get<JWTSettings>();
      var secret = Encoding.ASCII.GetBytes(tokenSettings.SecretKey);
      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(x =>
      {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(secret),
          ValidIssuer = tokenSettings.Issuer,
          ValidAudience = tokenSettings.Audience,
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          RequireExpirationTime = false,
          ClockSkew = TimeSpan.Zero
        };
        x.Events = new JwtBearerEvents
        {
          OnAuthenticationFailed = context =>
            {
              if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
              {
                context.Response.Headers.Add("Token-Expired", "true");
              }
              return Task.CompletedTask;
            }
        };
      });

      //Map user-secrets to POCO classes
      services.Configure<DatabaseSecrets>(Configuration.GetSection("Database"));
      services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.EnvironmentName == "Development")
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseRouting();
      app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseHttpsRedirection();
      app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
    }
  }
}
