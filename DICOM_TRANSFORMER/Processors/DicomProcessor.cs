using Antidote.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Antidote
{
    public class DicomProcessor : IProcessor
    {
        private FileRepository repo;

        public DicomProcessor()
        {
            repo = new FileRepository();
        }

        public void Process(string filename)
        {
            var vectors = GetVectorsFromFile(filename);

            repo.SaveVectors(vectors, filename);

        }

        private List<Vector2> GetVectorsFromFile(string filename)
        {
            var vectors = new List<Vector2>();

            string[] splitData = new string[0];
            var found = false;
            string line;
            byte[] vectorTag = GenerateHeader();

            using (StreamReader file = new StreamReader(filename))
            {
                

                //find data line
                while ((line = file.ReadLine()) != null && !found)
                {
                    if (line.Length >= 7  && IsDicomVectorHeader(line, vectorTag))
                    {
                        line = line.Substring(7, line.Length - 7);
                        splitData = line.Split(@"\");
                        found = true;
                    }
                }
            }

            if (found)
            {
                //Get the data into vectors
                vectors = ExtractVectorData(splitData);

                //close the vector by added first as last if the current first/last are not equal
                if (ApplicationConfig.dicomCloseVecotors && !vectors.First().Equals(vectors.Last()))
                {
                    vectors.Add(vectors.First());
                }
            }

            return vectors;
        }

        private  bool IsDicomVectorHeader(string line, byte[] vectorheader)
        {
            var headerToMatch = System.Text.Encoding.UTF8.GetString(vectorheader).ToCharArray();
            var fileHeacder = line.Substring(0, 7).ToCharArray();
            var result = Compare(fileHeacder, headerToMatch);
            return result;
        }

  
        private bool Compare(char[] fileHeader, char[] headerToMatch)
        {
            int idx = 0;
            foreach(var item in fileHeader)
            {
                if (item != headerToMatch[idx])
                { return false; }
                idx++;
            }

            return true;
        }

        private byte[] GenerateHeader()
        {
            byte[] dicomVectorHeader = { 0x30, 0x06, 0x01, 0xB6,0x10, 0x00, 0x00 };
            return dicomVectorHeader;
        }

        private List<Vector2> ExtractVectorData(string[] values)
        {
            List<Vector2> vectors = new List<Vector2>();
           
            for(int idx = 0; idx<values.Length; idx+=2)
            {
                vectors.Add(new Vector2 { X = (float)Math.Round(float.Parse(values[idx]),2 ) , Y = (float)Math.Round(float.Parse(values[idx+1]),2) });
            }

            return vectors;
        }

    }
}
