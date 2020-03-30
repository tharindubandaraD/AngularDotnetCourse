using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DatingApp.API
{
    public class Program
    {
        //controlls has endpoints - this will list the roots - it first comes to main method
        public static void Main(string[] args)
        {
            
            CreateHostBuilder(args).Build().Run();
        }

        //create hostbuilder - this will configure some defaults - like which server should run -- kastle server
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //this will read from config file and which cantain app.settings - it has login infomation
                    webBuilder.UseStartup<Startup>();
                });
    }
}
