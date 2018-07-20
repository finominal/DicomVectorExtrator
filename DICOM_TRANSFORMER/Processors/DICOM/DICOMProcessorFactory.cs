using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Antidote.Processors.Factory
{
    public  class DICOMProcessorFactory
    {
      //  private ILogger logger { get; set; }
      private List<IProcessor> DicomProcessors { get; set; }

        public DICOMProcessorFactory()
        {
            // logger = new ILogger();
            DicomProcessors = GetDicomProcessors();
        }


        public  IProcessor Get(string filename)
        {
            try
            {
                var processor = Inspect(filename);
            }
            catch (Exception e)
            {
                //log exception
            }
            return null;
        }

        private IProcessor Inspect(string filename)
        {
            foreach (var reader in DicomProcessors)
            {
                if (reader.IsValid(filename))
                {
                    return reader;
                }
            }
            return null;
        }


        //change to IOC
        private List<IProcessor> GetDicomProcessors()
        {
            var procesors = new List<IProcessor>();

            procesors.Add(new DicomProcessorA());

            return procesors;
        }
    }
}
