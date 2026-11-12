using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business_Logic_Layer
{
    public enum Skilltype { Novice, Professional, Expert, Entrepreneur }
    public class JoinerDTO : PersonDTO
    {
        public Skilltype SkillLevel { get; set; }

       
    }
}
