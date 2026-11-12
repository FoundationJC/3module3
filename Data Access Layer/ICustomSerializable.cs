using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public interface ICustomSerializable
    {
        // Метод, який перетворить об'єкт на один рядок тексту (наприклад, "Ivan;20;CS")
        string ToCustomString();

        // Метод, який візьме рядок тексту і заповнить властивості об'єкта
        void FromCustomString(string data);
    }
}
