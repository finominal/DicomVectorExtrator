using Antidote;
using Antidote.Processors.Factory;
using Antidote.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Antidote
{
    public class Processor
    {

        private FileRepository fileRepository;
        private string sourceDirectory;

        public Processor()
        {
            fileRepository = new FileRepository();
            sourceDirectory = ApplicationConfig.sourceDirectory;
        }

        public void Run()
        {
            var fileEntries = fileRepository.DirSearch(sourceDirectory);

            if (fileEntries.Count == 0)
            {
                fileRepository.WriteLog("No New Files To process.");
                Console.WriteLine("No New Files To process.");
            }

            foreach (var filename in fileEntries)
            {
 
                IProcessor processor;

                try
                {

                    if (filename.Substring(filename.Length - 3, 3) == "DAT")
                    {
                        processor = new DatProcessor();

                    }
                    else if (filename.Substring(filename.Length - 3, 3) == "dcm")
                    {
                        var factory = new DICOMProcessorFactory();
                        processor = factory.Get(filename);
                    }
                    else
                    {
                        continue;
                    }

                    fileRepository.WriteLog("Processing file " + filename);
                    Console.WriteLine("Processing file " + filename);

                    processor.Process(filename);
                    fileRepository.Complete(filename);

                    fileRepository.WriteLog("Completed file " + filename);
                    Console.WriteLine("Completed file " + filename);

                }
                catch (Exception e)
                {
                    fileRepository.WriteLog(e.ToString());
                    Console.WriteLine(e.ToString());
                }
            }


            Console.WriteLine("Processing Finished!");
            Thread.Sleep(3000);
        }
    }
}