using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public class CustomStringDTO
    {
        public string Value { get; set; } = string.Empty;
        public uint Length { get; set; }
        public int CodeDifference { get; set; }
        public bool IsForwardDirection { get; set; }

    }
}
