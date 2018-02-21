using Microsoft.Extensions.Configuration;
using System.IO;

namespace Antidote
{
    public class AppConfig
    {

        public static  IConfiguration Configuration { get; set; }
        
        public AppConfig()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            
        }

    }
}
