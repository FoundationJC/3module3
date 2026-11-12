using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    // 1. Спочатку оголошуємо список можливих статей
    [Serializable]
    public enum GenderType { Male, Female }

    [Serializable]
    public abstract class Person : ICustomSerializable
    {
        public Person() { }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Residence { get; set; } = string.Empty;

        // 2. Створюємо властивість типу GenderType
        public GenderType Gender { get; set; }

        // 3. Робимо метод публічним і додаємо логіку пакування
        public virtual string ToCustomString()
        {
            return $"{FirstName};{LastName};{Residence};{Gender}";
        }

        // 4. Робимо метод публічним і додаємо логіку розпакування
        public virtual void FromCustomString(string data)
        {
            string[] parts = data.Split(';');

            // Якщо є хоча б 4 частини, заповнюємо дані (Gender розпаковуємо через Enum.Parse)
            if (parts.Length >= 4)
            {
                FirstName = parts[0];
                LastName = parts[1];
                Residence = parts[2];
                Gender = Enum.Parse<GenderType>(parts[3]);
            }
        }
    }

}
