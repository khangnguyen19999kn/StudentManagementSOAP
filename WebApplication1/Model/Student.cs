using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Connection
{
    public class Student
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string dateBirth { get; set; }
        public string email { get; set; }
        public float gpa { get; set; }
        public string password { get; set; }
        // Các thuộc tính khác của sinh viên

        public Student()
        {

        }
    }
}