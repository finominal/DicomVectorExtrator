using Antidote;
using Antidote.Utility;
using System;
using System.Collections.Generic;
using System.IO;

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

            if (fileEntries.Count == 0) fileRepository.WriteLog("No New Files To process.");
            
            foreach (var filename in fileEntries)
            {
                fileRepository.WriteLog("Processing file " + filename);

                IProcessor processor;

                try
                {

                    if (filename.Substring(filename.Length - 3, 3) == "DAT")
                    {
                        processor = new DatProcessor();
                        processor.Process(filename);

                    }
                    else if (filename.Substring(filename.Length - 3, 3) == "dcm")
                    {
                        processor = new DicomProcessor();
                        processor.Process(filename);
                    }

                    fileRepository.Archive(filename);

                    fileRepository.WriteLog("Completed file " + filename);

                }
                catch (Exception e)
                {
                    fileRepository.WriteLog(e);
                }
            }
        }
    }
}