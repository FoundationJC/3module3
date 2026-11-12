using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public class PersonNotFoundException : Exception
    {
        public PersonNotFoundException() { }

        // Цей конструктор дозволяє передати текст помилки
        public PersonNotFoundException(string message) : base(message) { }
    }
}
