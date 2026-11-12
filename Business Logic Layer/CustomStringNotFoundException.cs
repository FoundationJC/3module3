using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public class CustomStringNotFoundException : Exception
    {
        public CustomStringNotFoundException() { }

        // Цей конструктор дозволяє передати текст помилки
        public CustomStringNotFoundException(string message) : base(message) { }
    }
}
