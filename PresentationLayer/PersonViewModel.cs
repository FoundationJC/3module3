using Business_Logic_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public abstract class PersonViewModel
    {
        // ViewModel містить посилання на DTO (композиція)
        protected PersonDTO _personDto;

        public PersonViewModel(PersonDTO personDto)
        {
            _personDto = personDto;
        }

        // Віртуальний метод для красивого виведення, який нащадки зможуть доповнити
        public override string ToString()
        {
            return $"Ім'я: {_personDto.FirstName} {_personDto.LastName}, Стать: {_personDto.Gender}, Місто: {_personDto.Residence}";
        }
    }
}
