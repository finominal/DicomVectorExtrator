using Antidote;
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
                fileRepository.WriteLog("Processing file " + filename);
                Console.WriteLine("Processing file " + filename);

                IProcessor processor;

                try
                {

                    if (filename.Substring(filename.Length - 3, 3) == "DAT")
                    {
                        processor = new DatProcessor();

                    }
                    else if (filename.Substring(filename.Length - 3, 3) == "dcm")
                    {
                        processor = new DicomProcessor();

                    }
                    else
                    {
                        Console.WriteLine("SKIPPING: {0} is not a processable file. ", filename);
                         
                        continue;
                    }

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


            Console.WriteLine("All Done.");
            Console.ReadKey();
        }
    }
}