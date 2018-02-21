using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.IO;
using Antidote.Utility;

namespace Antidote
{
    public class FileRepository
    {

        private readonly string saveDirectory;

        public FileRepository()
        {
            saveDirectory = @"c:\MART\TRANSFORMED\";
        }

        internal void SaveVectors(List<Vector2> vectors, string filename, int applicatorSize = 0)
        {
            var destinationFileName =  GenerateDestinationName(filename, applicatorSize);

            if (!Directory.Exists(saveDirectory)) Directory.CreateDirectory(saveDirectory);

            using (var file = new StreamWriter(saveDirectory + destinationFileName))
            {
                foreach (var vector in vectors)
                {
                    string line = vector.X + ", " + vector.Y;
                    file.WriteLine(line);
                }
            }
        }

        private string GenerateDestinationName(string filename, int applicatorSize)
        {
            var file = Path.GetFileNameWithoutExtension(filename);
            return applicatorSize > 0 ?
             file + "_" + ApplicatorSizes.Applicators[applicatorSize] + ".csv" :
             file + ".csv";


        }
    }
}
