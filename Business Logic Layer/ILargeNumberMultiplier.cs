using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public interface ILargeNumberMultiplier
    {
        // Метод приймає два числа у вигляді тексту і повертає результат також текстом
        string MultiplyLargeNumbers(string number1, string number2);
    }
}
