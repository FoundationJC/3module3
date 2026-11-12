using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    [Serializable]
    public class Student : Person
    {
        // Додаємо порожній конструктор і для студента (щоб працював CustomProvider)
        public Student() { }

        public uint Course { get; set; }
        public int StudentCardId { get; set; }
        public uint RecordBookNumber { get; set; }

        // Перевизначаємо метод батька (override)
        public override string ToCustomString()
        {
            // base.ToCustomString() поверне рядок "Ім'я;Прізвище;Київ;Male"
            // А ми просто доклеюємо до нього дані студента
            return $"{base.ToCustomString()};{Course};{StudentCardId};{RecordBookNumber}";
        }

        public override void FromCustomString(string data)
        {
            // Спочатку просимо батька (Person) розпакувати свою частину (перші 4 властивості)
            base.FromCustomString(data);

            string[] parts = data.Split(';');

            // Оскільки перші 4 частини (індекси 0, 1, 2, 3) забрав Person,
            // наші дані студента лежать починаючи з індексу 4.
            if (parts.Length >= 7)
            {
                Course = uint.Parse(parts[4]);
                StudentCardId = int.Parse(parts[5]);
                RecordBookNumber = uint.Parse(parts[6]);
            }
        }
    }
}
