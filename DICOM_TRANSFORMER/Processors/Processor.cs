using Antidote;
using System.IO;

namespace DICOM_TRANSFORMER
{
    public class Processor
    {

        private FileRepository fileRepository;
        private string sourceDirectory = "c:/mart/";

        public void Run()
        {

            //connect to dropbox 
            //move to local
            var fileEntries = Directory.GetFiles(sourceDirectory);
           

            //foreach  file
            foreach (var filename in fileEntries)
            {
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

            }




            //move to dropbox
        }
    }
}