// See https://aka.ms/new-console-template for more information
using Business_Logic_Layer;
using Data_Access_Layer;
using System.Collections.Generic;
using System;



namespace PresentationLayer
{
    public class Menu
    {
        private readonly EntityService _entityService = new EntityService();
        private readonly MultiplicationService _multiplicationService = new MultiplicationService();

        public void MainMenu()
        {
            // 1. Встановлюємо кодування UTF-8 для правильного відображення та введення української мови
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ГОЛОВНЕ МЕНЮ ===");
                Console.WriteLine("1. Демонстрація серіалізації/десеріалізації (Масив vs Колекція)");
                Console.WriteLine("2. Обчислити кількість студенток 5-го курсу з Києва (Варіант 10)");
                Console.WriteLine("3. Додаткове вміння (Множення великих чисел)");
                Console.WriteLine("4. Згенерувати тестових студентів у файл students.json"); // НОВИЙ ПУНКТ
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть пункт: ");

                string? choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            RunPart1Demo();
                            break;
                        case "2":
                            RunVariant10Task();
                            break;
                        case "3":
                            RunMultiplyTask();
                            break;
                        case "4":
                            GenerateTestStudents(); // Виклик нового методу
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Невірний вибір. Натисніть Enter...");
                            Console.ReadLine();
                            break;
                    }
                }
                catch (PersonNotFoundException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nПомилка бізнес-логіки: {ex.Message}");
                    Console.ResetColor();
                    Console.ReadLine();
                }
                catch (InvalidFileFormatException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nПомилка формату файлу: {ex.Message}");
                    Console.ResetColor();
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nКритична помилка: {ex.Message}");
                    Console.ResetColor();
                    Console.ReadLine();
                }
            }
        }

        private void RunPart1Demo()
        {
            Console.WriteLine("\n=== Частина 1: Демонстрація серіалізації (6 об'єктів) ===");

            // 1. Створюємо масив з 6 об'єктів (Варіант 10)
            var stringsArray = new CustomStringDTO[6]
            {
                new CustomStringDTO { Value = "Hello", Length = 5, CodeDifference = 1, IsForwardDirection = true },
                new CustomStringDTO { Value = "World", Length = 5, CodeDifference = 2, IsForwardDirection = false },
                new CustomStringDTO { Value = "Csharp", Length = 6, CodeDifference = 3, IsForwardDirection = true },
                new CustomStringDTO { Value = "Kiev", Length = 4, CodeDifference = 1, IsForwardDirection = false },
                new CustomStringDTO { Value = "Student", Length = 7, CodeDifference = 2, IsForwardDirection = true },
                new CustomStringDTO { Value = "NAU", Length = 3, CodeDifference = 1, IsForwardDirection = false }
            };

            // 2. Створюємо колекцію (List) з тих самих 6 об'єктів
            var stringsList = new List<CustomStringDTO>(stringsArray);

            string filePath = "demo_strings.json";
            ProviderType format = ProviderType.JSON; // Можна дозволити користувачу обирати

            try
            {
                // ДЕМОНСТРАЦІЯ 1: Робота з масивом
                Console.WriteLine("\n[1] Зберігаємо масив у файл...");
                // Примітка: Тобі потрібно додати метод SaveCustomStrings(CustomStringDTO[], шлях, формат) у EntityService
                _entityService.SaveCustomStrings(stringsArray, filePath, format);

                Console.WriteLine("[2] Читаємо масив з файлу...");
                var loadedArray = _entityService.GetCustomStrings(filePath, format);
                Console.WriteLine($"Завантажено об'єктів у масив: {loadedArray.Length}");

                // ДЕМОНСТРАЦІЯ 2: Робота з колекцією
                // List можна легко перетворити в масив за допомогою .ToArray() для сумісності з DAL, 
                // або додати підтримку ICollection у провайдерах.
                Console.WriteLine("\n[3] Зберігаємо колекцію List у файл...");
                _entityService.SaveCustomStrings(stringsList.ToArray(), filePath, format);

                Console.WriteLine("[4] Читаємо дані у колекцію List...");
                var loadedList = new List<CustomStringDTO>(_entityService.GetCustomStrings(filePath, format));
                Console.WriteLine($"Завантажено об'єктів у колекцію: {loadedList.Count}");

                Console.WriteLine("\n[Демонстрація виконана успішно]");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Помилка під час демонстрації: {ex.Message}");
                Console.ResetColor();
            }

            

            Console.WriteLine("\n=== Демонстрація серіалізації та десеріалізації (Масиви vs Колекції) ===");

            // 1. СТУДЕНТИ (Масив та Колекція -> JSON)
            StudentDTO[] studentsArray =
            {
              new StudentDTO { FirstName = "Олена", LastName = "Коваленко", Residence = "Київ", Gender = Business_Logic_Layer.GenderType.Female, Course = 5, StudentCardId = 101, RecordBookNumber = 5001 },
              new StudentDTO { FirstName = "Тарас", LastName = "Шевченко", Residence = "Львів", Gender = Business_Logic_Layer.GenderType.Male, Course = 3, StudentCardId = 102, RecordBookNumber = 5002 }
            };
            List<StudentDTO> studentsList = new List<StudentDTO>(studentsArray);

            _entityService.SaveStudents(studentsArray, "students_array.json", ProviderType.JSON);
            StudentDTO[] restoredStudentsArray = _entityService.GetStudents("students_array.json", ProviderType.JSON);

            _entityService.SaveStudents(studentsList.ToArray(), "students_list.json", ProviderType.JSON);
            List<StudentDTO> restoredStudentsList = new List<StudentDTO>(_entityService.GetStudents("students_list.json", ProviderType.JSON));

            Console.WriteLine($"\n[StudentDTO] Відновлено з масиву ({restoredStudentsArray.Length} шт.) та колекції ({restoredStudentsList.Count} шт.):");
            foreach (var s in restoredStudentsArray)
                Console.WriteLine("  " + new StudentViewModel(s));

            // 2. ФОТОГРАФИ (Масив та Колекція -> XML)
            PhotographerDTO[] photographersArray =
            {
              new PhotographerDTO { FirstName = "Ірина", LastName = "Мельник", Residence = "Київ", Gender = Business_Logic_Layer.GenderType.Female, Camera = Business_Logic_Layer.CameraType.Mirrorless },
              new PhotographerDTO { FirstName = "Дмитро", LastName = "Бойко", Residence = "Одеса", Gender = Business_Logic_Layer.GenderType.Male, Camera = Business_Logic_Layer.CameraType.DSLR }
            };
            List<PhotographerDTO> photographersList = new List<PhotographerDTO>(photographersArray);

            _entityService.SavePhotographers(photographersArray, "photographers_array.xml", ProviderType.XML);
            PhotographerDTO[] restoredPhotographersArray = _entityService.GetPhotographers("photographers_array.xml", ProviderType.XML);

            _entityService.SavePhotographers(photographersList.ToArray(), "photographers_list.xml", ProviderType.XML);
            List<PhotographerDTO> restoredPhotographersList = new List<PhotographerDTO>(_entityService.GetPhotographers("photographers_list.xml", ProviderType.XML));

            Console.WriteLine($"\n[PhotographerDTO] Відновлено з масиву ({restoredPhotographersArray.Length} шт.) та колекції ({restoredPhotographersList.Count} шт.):");
            foreach (var p in restoredPhotographersArray)
                Console.WriteLine("  " + new PhotographerViewModel(p));

            // 3. СТОЛЯРИ (Масив та Колекція -> Custom)
            JoinerDTO[] joinersArray =
            {
              new JoinerDTO { FirstName = "Василь", LastName = "Кравчук", Residence = "Харків", Gender = Business_Logic_Layer.GenderType.Male, SkillLevel = Business_Logic_Layer.Skilltype.Expert },
              new JoinerDTO { FirstName = "Микола", LastName = "Савченко", Residence = "Дніпро", Gender = Business_Logic_Layer.GenderType.Male, SkillLevel = Business_Logic_Layer.Skilltype.Professional }
            };
            List<JoinerDTO> joinersList = new List<JoinerDTO>(joinersArray);

            _entityService.SaveJoiners(joinersArray, "joiners_array.txt", ProviderType.Custom);
            JoinerDTO[] restoredJoinersArray = _entityService.GetJoiners("joiners_array.txt", ProviderType.Custom);

            _entityService.SaveJoiners(joinersList.ToArray(), "joiners_list.txt", ProviderType.Custom);
            List<JoinerDTO> restoredJoinersList = new List<JoinerDTO>(_entityService.GetJoiners("joiners_list.txt", ProviderType.Custom));

            Console.WriteLine($"\n[JoinerDTO] Відновлено з масиву ({restoredJoinersArray.Length} шт.) та колекції ({restoredJoinersList.Count} шт.):");
            foreach (var j in restoredJoinersArray)
                Console.WriteLine("  " + new JoinerViewModel(j));

            // 4. ПОРІВНЯННЯ МАСИВУ ТА КОЛЕКЦІЇ (Вимога п.4 Частини 1)
            Console.WriteLine("\n--- Порівняння масиву та колекції List<T> ---");
            Console.WriteLine("• Масив (T[]): фіксований розмір, пряма підтримка провайдерами IDataProvider<T>.");
            Console.WriteLine("• Колекція (List<T>): динамічний розмір, зручне додавання/видалення, при передачі в DAL легко конвертується через .ToArray().");

            Console.WriteLine("\nНатисніть Enter для повернення в меню...");
            Console.ReadLine();

        }


        private void RunVariant10Task()
        {
            Console.Write("\nВведіть шлях/ім'я файлу (наприклад, students.json): ");
            string? path = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(path))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Шлях до файлу не може бути порожнім. Натисніть Enter...");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Оберіть формат (0 - JSON, 1 - XML, 2 - Binary, 3 - Custom): ");
            string? formatInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(formatInput) || !int.TryParse(formatInput, out int formatValue))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Формат повинен бути числом від 0 до 3. Натисніть Enter...");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }
            ProviderType format = (ProviderType)formatValue;


            int count = _entityService.GetTargetStudentsCount(path, format);
            Console.WriteLine($"\nКількість студенток 5-го курсу з Києва: {count}");
            Console.ReadLine();
        }

        private void RunMultiplyTask()
        {
            Console.Write("\nВведіть перше велике число: ");
            string? num1 = Console.ReadLine();
            Console.Write("Введіть друге велике число: ");
            string? num2 = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(num1) || string.IsNullOrWhiteSpace(num2))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Обидва числа повинні бути введені. Натисніть Enter...");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            string? result = _multiplicationService.MultiplyLargeNumbers(num1, num2);
            Console.WriteLine($"Результат множення: {result}");
            Console.ReadLine();
        }

        private void GenerateTestStudents()
        {
            // Створюємо масив студентів для перевірки твого варіанту
            var testStudents = new StudentDTO[]
            {
             // Підходить під умову (Дівчина, 5 курс, Київ) = 1
             new StudentDTO { FirstName = "Олена", LastName = "Коваленко", Residence = "Київ", Gender = Business_Logic_Layer.GenderType.Female, Course = 5, StudentCardId = 111, RecordBookNumber = 111 },
             // Підходить під умову (Дівчина, 5 курс, Київ) = 2
             new StudentDTO { FirstName = "Марія", LastName = "Іваненко", Residence = "Київ", Gender = Business_Logic_Layer.GenderType.Female, Course = 5, StudentCardId = 222, RecordBookNumber = 222 },
             // Не підходить (Хлопець)
             new StudentDTO { FirstName = "Іван", LastName = "Петренко", Residence = "Київ", Gender = Business_Logic_Layer.GenderType.Male, Course = 5, StudentCardId = 333, RecordBookNumber = 333},
             // Не підходить (4 курс)
             new StudentDTO { FirstName = "Анна", LastName = "Сидоренко", Residence = "Київ", Gender = Business_Logic_Layer.GenderType.Female, Course = 4, StudentCardId = 444, RecordBookNumber = 444 },
             // Не підходить (Львів)
             new StudentDTO { FirstName = "Юлія", LastName = "Мельник", Residence = "Львів", Gender = Business_Logic_Layer.GenderType.Female, Course = 5, StudentCardId = 555, RecordBookNumber = 555 }
            };

            try
            {
                // Викликаємо твій метод з EntityService для збереження
                _entityService.SaveStudents(testStudents, "students.json", ProviderType.JSON);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nТестовий файл 'students.json' успішно згенеровано! Тепер можна виконувати пункт 2.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nПомилка при створенні файлу: {ex.Message}");
                Console.ResetColor();
            }

            Console.ReadLine();
        }

    }   
}