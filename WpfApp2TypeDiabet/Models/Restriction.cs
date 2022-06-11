using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2TypeDiabet.Models
{
    public class Restriction
    {
        public int id { get; set; }
        public string RestrictionName { get; set; }
        public double Amount { get; set; }
        public bool IsDefault { get; set; }
        public string Comparator { get; set; }
        public string Unit { get; set; }
    }
}
