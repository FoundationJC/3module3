using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    [Serializable]
    // 1. Вказуємо, що клас реалізує наш інтерфейс
    public class CustomString : ICustomSerializable
    {
        // 2. Виправляємо попередження через = string.Empty;
        public string Value { get; set; } = string.Empty;
        public uint Length { get; set; }
        public int CodeDifference { get; set; }
        public bool IsForwardDirection { get; set; }

        public CustomString() { }

        



       

        // ====================================================
        // РЕАЛІЗАЦІЯ ІНТЕРФЕЙСУ ICustomSerializable
        // ====================================================

        // Метод для пакування об'єкта в 1 рядок тексту (розділяємо крапкою з комою)
        public string ToCustomString()
        {
            return $"{Value};{Length};{CodeDifference};{IsForwardDirection}";
        }

        // Метод для розпакування тексту назад у властивості
        public void FromCustomString(string data)
        {
            // Розбиваємо рядок на частини по символу ';'
            string[] parts = data.Split(';');

            // Якщо частин 4, заповнюємо властивості (перетворюючи текст у потрібні типи)
            if (parts.Length == 4)
            {
                Value = parts[0];
                Length = uint.Parse(parts[1]);
                CodeDifference = int.Parse(parts[2]);
                IsForwardDirection = bool.Parse(parts[3]);
            }
        }




    }
}
