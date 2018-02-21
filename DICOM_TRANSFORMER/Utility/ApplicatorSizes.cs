using System;
using System.Collections.Generic;
using System.Text;

namespace Antidote.Utility
{
    public static class ApplicatorSizes
    {
        public static Dictionary<int, string> Applicators;

        static  ApplicatorSizes()
        {
            Populate();
        }

        private static void Populate()
        {
            Applicators = new Dictionary<int, string>();
            Applicators.Add(300, "6x6");
            Applicators.Add(500, "10x10");
            Applicators.Add(700, "14x14");
            Applicators.Add(750, "15x15");
            Applicators.Add(1000, "20x20");
            Applicators.Add(1250, "25x25");
        }

    }

}
