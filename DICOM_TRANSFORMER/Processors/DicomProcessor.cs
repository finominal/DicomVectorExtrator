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
                        splitData = line.Split(@"\"); vectors = ExtractVectorData(file);
                        found = true;
                    }
                }
            }

            if (found)
            {
                //Get the data into vectors
                for (int i = 0; i < splitData.Count(); i = i + 2)
                {
                    vectors.Add(new Vector2 { X = float.Parse(splitData[i]), Y = float.Parse(splitData[i + 1]) });
                }

                //close the vector by added first as last if the current first/last are not equal
                if (ApplicationConfig.dicomCloseVecotors && !vectors.First().Equals(vectors.Last()))
                {
                    vectors.Add(vectors.First());
                }
            }

            return vectors;
        }

        private  bool IsDicomVectorHeader(string line, byte[] vectorTag)
        {
            var headerAsCharArray = System.Text.Encoding.UTF8.GetString(vectorTag).ToCharArray();
            var fileHeacder = line.Substring(0, 7).ToCharArray();
            var result =  fileHeacder == headerAsCharArray;
            return result;
        }

        private byte[] GenerateHeader()
        {
            byte[] dicomVectorHeader = { 0x30, 0x06, 0x01, 0xB6, 0x00, 0x00 };
            return dicomVectorHeader;
        }

        private List<Vector2> ExtractVectorData(StreamReader file)
        {
            string line;

            List<Vector2> vectors = new List<Vector2>();

            while ((line = file.ReadLine()) != "17") //find 17 which marks the end of the data
            {
                string[] split = line.Split(" ");
                vectors.Add(new Vector2 { X = int.Parse(split[0]), Y = int.Parse(split[1]) });
            }

            return vectors;
        }

    }
}
