using Data_Access_Layer; // BLL має бачити DAL
using System.Linq; // Додаємо LINQ для зручного пошуку
using System.Reflection;
using System.Security.Policy;


namespace Business_Logic_Layer
{
    // Клас не потрібно робити узагальненим (generic), він просто керує бізнес-логікою
    public class EntityService
    {
        // Метод для виконання твого варіанту: →Обчислити кількість студенток 5-го курсу, які постійно проживають у Києві. Отримати їх дані з файлу←
        // Він приймає шлях і тип файлу (які прийдуть з Menu), а повертає кількість (int)
        public int GetTargetStudentsCount(string filePath, ProviderType format)
        {
            // 1. СТВОРЕННЯ КОНТЕКСТУ (Композиція замість спадкування)
            // Ми кажемо DAL: "Підготуй інструменти для роботи зі студентами у цьому файлі"
            var context = new EntityContext<Student>(filePath, format);

            // 2. ОТРИМАННЯ ДАНИХ (DAL дістає їх з файлу)
            Student[] studentsFromDb = context.Load();

            // Перевірка на порожнечу, використовуючи твій новий виняток!
            if (studentsFromDb == null || studentsFromDb.Length == 0)
            {
                throw new PersonNotFoundException("У файлі немає студентів або файл порожній.");
            }

            // 3. МАПІНГ (Mapping) - переклад об'єктів DAL у "чисті" об'єкти BLL
            var studentDTOs = new List<StudentDTO>();

            foreach (var student in studentsFromDb)
            {
                var dto = new StudentDTO
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Residence = student.Residence,
                    // Тут зверни увагу: якщо у тебе GenderType є і в DAL, і в BLL, 
                    // нам треба привести тип (cast) або написати логіку перекладу
                    Gender = (Business_Logic_Layer.GenderType)student.Gender,
                    Course = student.Course,
                    StudentCardId = student.StudentCardId,
                    RecordBookNumber = student.RecordBookNumber
                };
                studentDTOs.Add(dto);
            }

            // 4. БІЗНЕС-ЛОГІКА (Твій 10 варіант)
            // За допомогою LINQ (методу .Where) відфільтруй список studentDTOs.
            // Тобі треба знайти тих, у кого:
            // Gender == GenderType.Female І (&&) Course == 5 І (&&) Residence == "Київ"

            var targetStudents = studentDTOs.Where(s => s.Gender == GenderType.Female && s.Course == 5 && s.Residence == "Київ").ToList();

            // 5. ПОВЕРНЕННЯ РЕЗУЛЬТАТУ
            // Метод має повернути кількість знайдених студенток
            return targetStudents.Count;
        }

        // Цей метод згодиться, коли з Menu (PL) ми захочемо зберегти нових студентів у файл
        public void SaveStudents(StudentDTO[] dtos, string filePath, ProviderType format)
        {
            var context = new EntityContext<Student>(filePath, format);

            // Тут потрібен зворотний мапінг (StudentDTO -> Student)
            var dalStudents = new Student[dtos.Length];

            for (int i = 0; i < dtos.Length; i++)
            {
                dalStudents[i] = new Student
                {
                    FirstName = dtos[i].FirstName,
                    LastName = dtos[i].LastName,
                    Residence = dtos[i].Residence,
                    Gender = (Data_Access_Layer.GenderType)dtos[i].Gender,
                    Course = dtos[i].Course,
                    StudentCardId = dtos[i].StudentCardId,
                    RecordBookNumber = dtos[i].RecordBookNumber
                };
            }

            // Віддаємо масив сутностей DAL контексту для збереження
            context.Save(dalStudents);
        }

        public void SaveCustomStrings(CustomStringDTO[] dtos, string filePath, ProviderType format)
        {
            // 1. Створюємо контекст для CustomString
            var context = new EntityContext<CustomString>(filePath, format);

            // 2. Зворотний мапінг (DTO -> DAL)
            var dalStrings = new CustomString[dtos.Length];
            for (int i = 0; i < dtos.Length; i++)
            {
                dalStrings[i] = new CustomString
                {
                    Value = dtos[i].Value,
                    Length = dtos[i].Length,
                    CodeDifference = dtos[i].CodeDifference,
                    IsForwardDirection = dtos[i].IsForwardDirection
                };
            }

            // 3. Віддаємо дані DAL-рівню для збереження у файл
            context.Save(dalStrings);
        }

        public StudentDTO[] GetStudents(string filePath, ProviderType format)
        {
            var context = new EntityContext<Student>(filePath, format);
            Student[] data = context.Load();

            if (data == null || data.Length == 0)
                throw new PersonNotFoundException($"У файлі '{filePath}' не знайдено студентів.");

            return data.Select(s => new StudentDTO
            {
                FirstName = s.FirstName,
                LastName = s.LastName,
                Residence = s.Residence,
                Gender = (Business_Logic_Layer.GenderType)s.Gender,
                Course = s.Course,
                StudentCardId = s.StudentCardId,
                RecordBookNumber = s.RecordBookNumber
            }).ToArray();
        }

        // ================= ФОТОГРАФИ =================
        public void SavePhotographers(PhotographerDTO[] dtos, string filePath, ProviderType format)
        {
            var context = new EntityContext<Photographer>(filePath, format);
            var dalItems = dtos.Select(p => new Photographer
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Residence = p.Residence,
                Gender = (Data_Access_Layer.GenderType)p.Gender,
                Camera = (Data_Access_Layer.CameraType)p.Camera
            }).ToArray();

            context.Save(dalItems);
        }

        public PhotographerDTO[] GetPhotographers(string filePath, ProviderType format)
        {
            var context = new EntityContext<Photographer>(filePath, format);
            Photographer[] data = context.Load();

            if (data == null || data.Length == 0)
                throw new PersonNotFoundException($"У файлі '{filePath}' не знайдено фотографів.");

            return data.Select(p => new PhotographerDTO
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Residence = p.Residence,
                Gender = (Business_Logic_Layer.GenderType)p.Gender,
                Camera = (Business_Logic_Layer.CameraType)p.Camera
            }).ToArray();
        }

        // ================= СТОЛЯРИ =================
        public void SaveJoiners(JoinerDTO[] dtos, string filePath, ProviderType format)
        {
            var context = new EntityContext<Joiner>(filePath, format);
            var dalItems = dtos.Select(j => new Joiner
            {
                FirstName = j.FirstName,
                LastName = j.LastName,
                Residence = j.Residence,
                Gender = (Data_Access_Layer.GenderType)j.Gender,
                SkillLevel = (Data_Access_Layer.Skilltype)j.SkillLevel
            }).ToArray();

            context.Save(dalItems);
        }

        public JoinerDTO[] GetJoiners(string filePath, ProviderType format)
        {
            var context = new EntityContext<Joiner>(filePath, format);
            Joiner[] data = context.Load();

            if (data == null || data.Length == 0)
                throw new PersonNotFoundException($"У файлі '{filePath}' не знайдено столярів.");

            return data.Select(j => new JoinerDTO
            {
                FirstName = j.FirstName,
                LastName = j.LastName,
                Residence = j.Residence,
                Gender = (Business_Logic_Layer.GenderType)j.Gender,
                SkillLevel = (Business_Logic_Layer.Skilltype)j.SkillLevel
            }).ToArray();
        }


        // Метод для роботи з кастомними рядками у BLL
        public CustomStringDTO[] GetCustomStrings(string filePath, ProviderType format)
        {
            var context = new EntityContext<CustomString>(filePath, format);
            CustomString[] stringsFromDb; // Тільки оголошуємо змінну тут

            try
            {
                // Спробуємо завантажити дані з DAL
                stringsFromDb = context.Load();
            }
            catch (FormatException ex) // Ловимо стандартну помилку з DAL
            {
                // BLL "перекладає" помилку інфраструктури у твій кастомний виняток
                throw new InvalidFileFormatException("Файл має невірний формат або дані пошкоджені.", ex);
            }

            // ПЕРЕВІРКА: якщо масив порожній або null — викидаємо CustomStringNotFoundException
            if (stringsFromDb == null || stringsFromDb.Length == 0)
            {
                throw new CustomStringNotFoundException($"У файлі '{filePath}' не знайдено жодного запису CustomString.");
            }

            // Мапінг у DTO (DAL -> BLL)
            var dtos = new CustomStringDTO[stringsFromDb.Length];
            for (int i = 0; i < stringsFromDb.Length; i++)
            {
                dtos[i] = new CustomStringDTO
                {
                    Value = stringsFromDb[i].Value,
                    Length = stringsFromDb[i].Length,
                    CodeDifference = stringsFromDb[i].CodeDifference,
                    IsForwardDirection = stringsFromDb[i].IsForwardDirection
                };
            }

            return dtos;
        }
        // У класі EntityService.cs (BLL)
        public void EncryptCustomString(CustomStringDTO dto)
        {
            char[] chars = dto.Value.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (dto.IsForwardDirection)
                    chars[i] = (char)(chars[i] + dto.CodeDifference);
                else
                    chars[i] = (char)(chars[i] - dto.CodeDifference);
            }
            dto.Value = new string(chars);
        }

        public void DecryptCustomString(CustomStringDTO dto)
        {
            char[] chars = dto.Value.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (dto.IsForwardDirection)
                    chars[i] = (char)(chars[i] - dto.CodeDifference);
                else
                    chars[i] = (char)(chars[i] + dto.CodeDifference);
            }
            dto.Value = new string(chars);
        }
        public string StringOutput(CustomStringDTO dto)
        {
            return $"Value:{dto.Value}; Length:{dto.Length}; CodeDifference:{dto.CodeDifference}";
        }
    }
}
