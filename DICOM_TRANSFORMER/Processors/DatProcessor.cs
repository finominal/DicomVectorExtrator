
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Drawing;
using Antidote.Utility;
using System.Linq;

namespace Antidote
{
    public class DatProcessor : IProcessor
    {
        public FileRepository repo   = new FileRepository();
        public int applicatorSize = 0;
        public int angleOfRotation = -90;


        public void Process(string filename)
        {

            //DAT
            var vectors = GetVectorsFromFile(filename);
            vectors = Transform(vectors);
            repo.SaveVectors(vectors, filename, applicatorSize);

        }

        private List<Vector2> Transform(List<Vector2> data)
        {
            var result = new List<Vector2>();
            foreach (var vector in data)
            {
                result.Add( Vectors.Rotate(vector, angleOfRotation));
            }
            return result;
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

            //Get Plate  size
            var size = file.ReadLine();
            string[] splitsize = size.Split(" ");
            applicatorSize = int.Parse(splitsize[0]);

            return vectors;
        }

    }
}
