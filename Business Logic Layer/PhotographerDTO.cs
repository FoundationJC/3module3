using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business_Logic_Layer
{
    public enum CameraType { Mirrorless, DSLR, Compact, Bridge }
    public class PhotographerDTO : PersonDTO
    {
        public CameraType Camera { get; set; }

        
    }
}
