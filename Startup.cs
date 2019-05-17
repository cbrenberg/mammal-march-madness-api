using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MMM_Bracket.API.Domain.Repositories;
using MMM_Bracket.API.Domain.Services;
using MMM_Bracket.API.Persistence.Contexts;
using MMM_Bracket.API.Persistence.Repositories;
using MMM_Bracket.API.Services;
using AutoMapper;
using JWT;

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
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      services.AddAutoMapper();

      services.AddEntityFrameworkNpgsql().AddDbContext<mmm_bracketContext>().BuildServiceProvider();

      services.AddScoped<IAnimalRepository, AnimalRepository>();
      services.AddScoped<IAnimalService, AnimalService>();

      services.AddScoped<ICategoryRepository, CategoryRepository>();
      services.AddScoped<ICategoryService, CategoryService>();

      services.AddScoped<IParticipantRepository, ParticipantRepository>();
      services.AddScoped<IParticipantService, ParticipantService>();

      services.AddScoped<ITokenAuthenticationService, TokenAuthenticationService>();

      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IUserService, UserService>();

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
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.SecretKey)),
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
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
      app.UseAuthentication();
      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}
