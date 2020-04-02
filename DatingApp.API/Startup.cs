<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DatingApp.API
{
    //configurations injected into this cs
    public class Startup
    {
        //allows to access if we want to these settings that are inside our configuration
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //dependancy injection container
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<DataContext>(x => x.UseSqlite
            (Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            //this is we using to register our repo because create one instance inside scope
            services.AddScoped<IAuthRepository, AuthRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //this all are act like middle ware 
            if (env.IsDevelopment())
            {
                //exception caught in here - global exception
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            //rooting in here 
            app.UseRouting();

            //add cors as a service - no any securit in this piece of code. - then we can allow api values in angular application
            //tell to trust this connection to browser
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            
            //authorizations in here 
            app.UseAuthorization();

            //map all controlles in here 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API
{
    //configurations injected into this cs
    public class Startup
    {
        //allows to access if we want to these settings that are inside our configuration
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //dependancy injection container
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<DataContext>(x => x.UseSqlite
            (Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            //this is we using to register our repo because create one instance inside scope
            services.AddScoped<IAuthRepository, AuthRepository>();
            //we are saying what authntication we using         
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer( options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer =false,
                    ValidateAudience = false
                };
            });
                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //this all are act like middle ware 
            if (env.IsDevelopment())
            {
                //exception caught in here - global exception
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            //rooting in here 
            app.UseRouting();

            //add cors as a service - no any securit in this piece of code. - then we can allow api values in angular application
            //tell to trust this connection to browser
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            //authentication in here
            app.UseAuthentication();
            //authorizations in here 
            app.UseAuthorization();

            //map all controlles in here 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
>>>>>>> 02-04-2020 Code Commit
