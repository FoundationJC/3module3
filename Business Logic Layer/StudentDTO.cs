using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public class StudentDTO : PersonDTO
    {
        public uint Course { get; set; }
        public int StudentCardId { get; set; }
        public uint RecordBookNumber { get; set; }
    }
}
