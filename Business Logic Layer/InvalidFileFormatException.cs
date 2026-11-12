using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public class InvalidFileFormatException : Exception, ISerializable
    {
        public InvalidFileFormatException() { }

        public InvalidFileFormatException(string message, Exception innerException) : base(message, innerException) { }
    }
}
