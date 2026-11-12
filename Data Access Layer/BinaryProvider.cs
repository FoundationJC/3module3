using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class BinaryProvider<T> : IDataProvider<T>
    {
        // Цей рядок приховує зелені підкреслення-попередження від компілятора
#pragma warning disable SYSLIB0011

        public void Serialize(T[] data, string filePath)
        {
            // 1. Створюємо "пакувальну машину"
            BinaryFormatter formatter = new BinaryFormatter();

            // 2. Відкриваємо "конвеєрну стрічку" (потік) до файлу у режимі створення.
            // Слово using гарантує, що потік автоматично закриється в кінці цього блоку!
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                // 3. Кажемо пакувальній машині: "Ось тобі стрічка (fs), ось деталі (data) - пакуй!"
                formatter.Serialize(fs, data);
            }
        }

        public T[] Deserialize(string filePath)
        {
            // 1. Створюємо "пакувальну машину" для розпакування
            BinaryFormatter formatter = new BinaryFormatter();

            // 2. Відкриваємо "конвеєрну стрічку" у режимі читання (або створення, якщо файлу ще нема)
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                // 3. Якщо файл порожній (ми його щойно створили), повертаємо порожній масив, 
                // щоб програма не впала з помилкою.
                if (fs.Length == 0)
                {
                    return new T[0];
                }

                // 4. Просимо розпакувати дані зі стрічки. 
                // Форматтер повертає загальний тип 'object', тому ми робимо явне приведення типів (T[]) - 
                // так само, як ти робив (char) у шифруванні рядка!
                return (T[])formatter.Deserialize(fs);
            }
        }
    }
}