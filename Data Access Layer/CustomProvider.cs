using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    // Обмеження: T має реалізувати наш інтерфейс і мати порожній конструктор
    public class CustomProvider<T> : IDataProvider<T> where T : ICustomSerializable, new()
    {
        public void Serialize(T[] data, string filePath)
        {
            // 1. Створюємо список для збереження текстових рядків
            List<string> lines = new List<string>();

            // 2. Проходимося циклом foreach по кожному об'єкту item у масиві data
            foreach (T item in data)
            {
                // 3. Викликаємо item.ToCustomString() і додаємо результат у список lines
                lines.Add(item.ToCustomString());
            }

            // 4. Записуємо всі рядки у файл одним махом
            File.WriteAllLines(filePath, lines);
        }

        public T[] Deserialize(string filePath)
        {
            if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
                return new T[0];

            // 1. Читаємо всі рядки з файлу
            string[] lines = File.ReadAllLines(filePath);

            // 2. Створюємо масив об'єктів такого ж розміру, як і кількість рядків
            T[] result = new T[lines.Length];

            // 3. Проходимося циклом for по масиву lines
            for (int i = 0; i < lines.Length; i++)
            {   // Перевірка: якщо рядок порожній або не містить роздільник ';', вважаємо файл пошкодженим
                if (string.IsNullOrWhiteSpace(lines[i]) || !lines[i].Contains(";"))
                {
                    // DAL викидає стандартний виняток .NET, він не потребує посилань на інші твої проєкти
                    throw new FormatException($"Рядок {i + 1} має невірний формат.");
                }

                // 4. Створюємо новий порожній об'єкт типу T
                result[i] = new T();

                // 5. Просимо об'єкт заповнити себе даними з поточного рядка lines[i]
                result[i].FromCustomString(lines[i]);
            }

            // 6. Повертаємо готовий масив об'єктів
            return result;
        }
    }
}

