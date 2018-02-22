using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace Antidote.Utility
{
    public static class ApplicationConfig 
    {

        public static string sourceDirectory { get; set; }
        public static string destinationDirectory { get; set; }
        public static string archiveDirectory { get; set; }
        public static string logDirectory { get; set; }
        public static int rotationAngle { get; set; }

        public static int dicomDataLineNumber { get; set; }

        public static void  Populate(IConfiguration config)
        {
            sourceDirectory = config["sourceDirectory"];

            destinationDirectory = config["destinationDirectory"];

            archiveDirectory = config["archiveDirectory"];

            logDirectory = config["logDirectory"];

            rotationAngle = int.Parse(config["rotationAngle"]);

            dicomDataLineNumber = int.Parse(config["dicomDataLineNumber"]);
        }
    }
}
