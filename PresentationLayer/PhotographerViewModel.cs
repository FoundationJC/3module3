using Business_Logic_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class PhotographerViewModel : PersonViewModel
    {
        private PhotographerDTO _photographerDto;

        public PhotographerViewModel(PhotographerDTO photographerDto) : base(photographerDto)
        {
            _photographerDto = photographerDto;
        }

        // Перевизначаємо виведення, додаючи специфічні дані фотографа
        public override string ToString()
        {
            return base.ToString() + $", Вид фотоапарату: {_photographerDto.Camera}";
        }
    }
}
