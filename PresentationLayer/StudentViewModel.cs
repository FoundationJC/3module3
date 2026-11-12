using Business_Logic_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class StudentViewModel : PersonViewModel
    {
        private StudentDTO _studentDto;

        public StudentViewModel(StudentDTO studentDto) : base(studentDto)
        {
            _studentDto = studentDto;
        }

        // Перевизначаємо виведення, додаючи специфічні дані студента
        public override string ToString()
        {
            return base.ToString() + $", Курс: {_studentDto.Course}, Студентський квиток: {_studentDto.StudentCardId}, Залікова книжка: {_studentDto.RecordBookNumber}";
        }
    }
}
