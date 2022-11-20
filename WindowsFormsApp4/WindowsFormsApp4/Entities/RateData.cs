using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4.Entities
{
    internal class RateData
    {
        public DateTime Date { get; set; }
        public string Currency { get; set; }

        public Decimal Value { get; set; }
    }
}
