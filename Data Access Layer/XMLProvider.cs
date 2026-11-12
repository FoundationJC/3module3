using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Data_Access_Layer
{
    public class XMLProvider<T>: IDataProvider<T>
    {
        public void Serialize(T[] data, string filePath)
        {
            // 1. Створюємо "пакувальну машину" для XML.
            // Їй обов'язково треба передати тип масиву: typeof(T[])
            XmlSerializer serializer = new XmlSerializer(typeof(T[]));

            // 2. Відкриваємо "конвеєрну стрічку" (FileStream) у режимі FileMode.Create
            // (Не забудь про конструкцію using!)
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fs, data);
            }
            // 3. Викликаємо метод Serialize у нашої машини, передаємо стрічку і дані
        }
        public T[] Deserialize(string filePath)
        {
            // 1. Перевірка на те, чи існує файл і чи не порожній він (беремо з JSON-провайдера)
            if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
            {
                return new T[0];
            }

            // 2. Знову створюємо "пакувальну машину" XmlSerializer з типом typeof(T[])
            XmlSerializer serializer = new XmlSerializer(typeof(T[]));
            // 3. Відкриваємо "конвеєрну стрічку" (FileStream) у режимі FileMode.Open
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                // 4. Викликаємо метод Deserialize. 
                // Пам'ятай, що як і в бінарному, його результат треба явно привести до типу (T[])
                var result = serializer.Deserialize(fs);
                return result is T[] array ? array : new T[0];
            }
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                