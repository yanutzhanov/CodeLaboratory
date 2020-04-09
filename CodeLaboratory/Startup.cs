using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CodeLaboratory.Helpers;
using Microsoft.IdentityModel.Tokens;
using CodeLaboratory.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using CodeLaboratory.Services.Abstract;
using CodeLaboratory.Services;
using CodeLaboratory.Data.Repositories.Abstract;
using CodeLaboratory.Data.Repositories;
using CodeLaboratory.Enteties;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using React.AspNet;
using JavaScriptEngineSwitcher.ChakraCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CodeLaboratory
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,

                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateLifetime = true,

                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Register");
                });

            services.AddCors();
            services.AddDbContext<CodeLabDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("mssqldb"));
            });

            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
