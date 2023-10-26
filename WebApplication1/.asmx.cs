using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebApplication1.Connection;

namespace StudentManagement
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
        public string GetStudent(int studentId)
        {
            return _dbHelper.GetStudent(studentId);
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
    
    

    }
}

