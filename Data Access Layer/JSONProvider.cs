using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class JSONProvider<T>: IDataProvider<T>
    {
        public void Serialize(T[] data, string filePath)
        {
            // 1. Перетворюємо масив у JSON-рядок
            string json = JsonSerializer.Serialize(data);

            // 2. Записуємо рядок у файл
            File.WriteAllText(filePath, json);
        }

        public T[] Deserialize(string filePath)
        {
            // 1. Якщо файлу немає або він порожній — повертаємо порожній масив
            if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
            {
                return new T[0];
            }

            // 2. Читаємо текст з файлу
            string json = File.ReadAllText(filePath);

            // 3. Відновлюємо масив з JSON (якщо повернувся null, віддаємо порожній масив)
            return JsonSerializer.Deserialize<T[]>(json) ?? new T[0];
        }
    }
}
