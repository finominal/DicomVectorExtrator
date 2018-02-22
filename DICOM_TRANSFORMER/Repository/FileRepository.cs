﻿using Microsoft.Extensions.FileProviders;
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

        private readonly string destinationDirectory;
        private readonly string archiveDirectory;
        private readonly string logDirectory;

        public FileRepository()
        {
            destinationDirectory = ApplicationConfig.destinationDirectory;
            archiveDirectory = ApplicationConfig.archiveDirectory;
            logDirectory = ApplicationConfig.logDirectory;
        }

        internal void SaveVectors(List<Vector2> vectors, string filename, int applicatorSize = 0)
        {
            var destinationFileName =  GenerateDestinationName(filename, applicatorSize);

            if (!Directory.Exists(destinationDirectory)) Directory.CreateDirectory(destinationDirectory);

            using (var file = new StreamWriter(destinationDirectory + destinationFileName))
            {
                foreach (var vector in vectors)
                {
                    string line = Math.Round(vector.X,2) + ", " + Math.Round(vector.Y,2);
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

        internal void Archive(string filename)
        {
            if (!Directory.Exists(archiveDirectory)) Directory.CreateDirectory(archiveDirectory);

            File.Move(filename, archiveDirectory + Path.GetFileName(filename));
        }

        public void WriteLog(string message)
        {
            if (!Directory.Exists(logDirectory)) Directory.CreateDirectory(logDirectory);

            var lines = new List<string>();
            lines.Add(DateTime.Now.ToLocalTime().ToString() + " " + message);

            File.AppendAllLines(logDirectory + "logs.txt", lines);
        }

    }
}
