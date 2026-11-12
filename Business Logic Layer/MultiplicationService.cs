using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public class MultiplicationService : ILargeNumberMultiplier
    {
        public string MultiplyLargeNumbers(string number1, string number2)
        {
            BigInteger n1 = BigInteger.Parse(number1);
            BigInteger n2 = BigInteger.Parse(number2);
            BigInteger result = n1 * n2;

            return result.ToString();
        }

    }
}
