using System;
using Microsoft.Extensions.Configuration;
using Antidote;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace DICOM_TRANSFORMER
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var processor = new Processor();
            processor.Run();


        }
    }
}

