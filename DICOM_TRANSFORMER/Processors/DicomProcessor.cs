using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace Antidote
{
    public class DicomProcessor : IProcessor
    {

        public void Process(string filename)
        {
            var vectors = GetVectorsFromFile(filename);
            //transform
            //save to file

        }

        private List<Vector2> GetVectorsFromFile(string filename)
        {
            string line;
            var vectors = new List<Vector2>();
            bool running = true;


            using (StreamReader file = new StreamReader(filename))
            {
                while ((line = file.ReadLine()) != null && running)
                {
                    if (line == "124")
                    {
                        vectors = ExtractVectorData(file);
                        running = false;
                    }
                }
            }

            return vectors;
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
