

using Microsoft.Extensions.Configuration;
using System.IO;
using Antidote.Utility;

namespace Antidote
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            ApplicationConfig.Populate(Configuration);


            Process();
        }

        private static void Process()
        {
            var processor = new Processor();
            processor.Run();
        }
    }
}

