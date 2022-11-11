using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsApp4
{
    internal class Ramen
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Brand Brand { get; set; }

        public int CounrtyFK { get; set; }

        public Country Country { get; set; }

        public double Rating { get; set; }
    }
}
