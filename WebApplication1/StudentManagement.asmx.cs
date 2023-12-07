using System.Collections.Generic;
using System.Web.Services;
using WebApplication1.Connection;

namespace WebApplication1
{
  
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class StudentManagement : System.Web.Services.WebService
    {

        private DatabaseHelper _dbHelper;

        public StudentManagement()
        {
            _dbHelper = new DatabaseHelper("Server=localhost;User ID=root;Password=Kn200499;Database=studentmanagement");
        }

        [WebMethod]
        public string GetAllStudents()
        {
            return _dbHelper.GetAllStudents();
        }

        [WebMethod]
        public string AddStudent(Student student)
        {
            return _dbHelper.addStudent(student);

        }
        private List<Student> students = new List<Student>();

        [WebMethod]
        public string GetStudent(string studentName)
        {
            return _dbHelper.GetStudent(studentName);
        }



        [WebMethod]
        public string UpdateStudent(Student student)
        {
            return _dbHelper.UpdateStudent(student);
        }

        [WebMethod]
        public string DeleteStudent(int studentId)
        {
            return _dbHelper.DeleteStudent(studentId);
        }
        [WebMethod]
        public string GetGenderCount()
        {
            return _dbHelper.GetGenderCounts();
        }

        [WebMethod]
        public string GetStudentStatistics()
        {
            return _dbHelper.GetStudentStatistics();
        }

    }
}
