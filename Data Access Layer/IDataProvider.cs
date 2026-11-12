using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    // <T> означає Type (Будь-який тип даних, який ми вкажемо пізніше)
    public interface IDataProvider<T> 
    {
        // Метод для збереження масиву будь-яких об'єктів у файл
        void Serialize(T[] data, string filePath);

        // Метод для читання з файлу масиву будь-яких об'єктів
        T[] Deserialize(string filePath);
    }
}
