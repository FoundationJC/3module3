// See https://aka.ms/new-console-template for more information
using System;
using PresentationLayer; // Підключаємо простір імен, де лежить твій Menu.cs

namespace Presentation_Layer // Назва твого стартового проекту
{
    class Program
    {
        static void Main(string[] args)
        {
            // Створюємо екземпляр класу Menu
            Menu menu = new Menu();

            // Викликаємо головне меню (і більше НІЧОГО в Main не пишемо!)
            menu.MainMenu();
        }
    }
}
