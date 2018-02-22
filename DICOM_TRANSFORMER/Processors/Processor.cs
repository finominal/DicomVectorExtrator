using Antidote;
using Antidote.Utility;
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

            //connect to dropbox 
            //move to local
            var fileEntries = Directory.GetFiles(sourceDirectory);
           
            if(fileEntries.Length == 0) fileRepository.WriteLog("No New Files To process.");
            //foreach  file
            foreach (var filename in fileEntries)
            {
                fileRepository.WriteLog("Processing file " + filename);

                IProcessor processor;

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


        }
    }
}