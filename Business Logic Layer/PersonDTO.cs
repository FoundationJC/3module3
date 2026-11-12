using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public enum GenderType { Male, Female }
    public abstract class PersonDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Residence { get; set; } = string.Empty;
        public GenderType Gender { get; set; }
    }
}
