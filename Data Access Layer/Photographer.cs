using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    [Serializable]
    public enum CameraType { Mirrorless, DSLR, Compact, Bridge }
    [Serializable]
    public class Photographer: Person
    {
        public Photographer() { }

        public CameraType Camera { get; set; }

        // Перевизначаємо метод батька (override)
        public override string ToCustomString()
        {
            // base.ToCustomString() поверне рядок "Ім'я;Прізвище;Київ;Male"
            // А ми просто доклеюємо до нього дані фотографа
            return $"{base.ToCustomString()};{Camera}";
        }

        public override void FromCustomString(string data)
        {
            // Спочатку просимо батька (Person) розпакувати свою частину (перші 4 властивості)
            base.FromCustomString(data);

            string[] parts = data.Split(';');

            // Оскільки перші 4 частини (індекси 0, 1, 2, 3) забрав Person,
            // наші дані фотографа лежать починаючи з індексу 4.
            if (parts.Length >= 5)
            {
                Camera = Enum.Parse<CameraType>(parts[4]);
            }
        }


    }
}
