using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public enum ProviderType { JSON, XML, Binary, Custom}
    public class EntityContext<T> where T : class, ICustomSerializable, new()
    {
        // 3. Приватні поля для шляху до файлу та провайдера
        private readonly string _filePath;
        private readonly IDataProvider<T> _provider;

        // 4. Конструктор
        public EntityContext(string filePath, ProviderType type)
        {
            _filePath = filePath;

            // Вибираємо правильний провайдер залежно від переданого типу
            switch (type)
            {
                case ProviderType.Binary:
                    // Напиши тут створення BinaryProvider
                    _provider = new BinaryProvider<T>() { };
                    break;
                case ProviderType.JSON:
                    _provider = new JSONProvider<T>() { };
                    break;
                case ProviderType.XML:
                    _provider = new XMLProvider<T>() { };
                    break;
                case ProviderType.Custom:
                    _provider = new CustomProvider<T>() { };
                    break;
                default:
                    throw new ArgumentException("Невідомий тип провайдера");
            }
        }

        // Метод для читання даних з файлу
        public T[] Load()
        {
            // Виклич метод Deserialize у твого провайдера (_provider), 
            


            // передай йому _filePath і поверни результат

            return _provider.Deserialize(_filePath); // Заміни цю заглушку
        }

        // Метод для збереження (оновлення) даних у файл
        public void Save(T[] data)
        {
            // Виклич метод Serialize у твого провайдера
            _provider.Serialize(data, _filePath);
        }

        // Метод для видалення файлу БД
        public void DeleteFile()
        {
            // Напиши перевірку: якщо файл існує (File.Exists), то видали його (File.Delete)
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }
    }
}

