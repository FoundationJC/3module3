using Business_Logic_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class CustomStringViewModel
    {
        private CustomStringDTO _customstringDto;

        public CustomStringViewModel(CustomStringDTO customstringDto) 
        {
            _customstringDto = customstringDto;

        }

        public override string ToString()
        {
            return $"Значення: {_customstringDto.Value}, Довжина: {_customstringDto.Length}, Різниця кодів: {_customstringDto.CodeDifference}, Напрямок: {_customstringDto.IsForwardDirection}";
        }
    }
}
