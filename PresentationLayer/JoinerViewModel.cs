using Business_Logic_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class JoinerViewModel : PersonViewModel
    {
        private JoinerDTO _joinerDto;

        public JoinerViewModel(JoinerDTO joinerDto) : base(joinerDto)
        {
            _joinerDto = joinerDto;
        }

        // Перевизначаємо виведення, додаючи специфічні дані столяра
        public override string ToString()
        {
            return base.ToString() + $", Рівень майстерності: {_joinerDto.SkillLevel}";
        }
    }
}
