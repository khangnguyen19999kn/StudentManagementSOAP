using Dapper;
using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Connection;
using WebApplication1.Model;

public class DatabaseHelper
{
    private string _connectionString;

    public DatabaseHelper(string connectionString)
    {
        _connectionString = connectionString;
    }
    private bool IsIdExists(MySqlConnection connection, int id)
    {
        string query = "SELECT COUNT(*) FROM student WHERE id = @id";
        var parameters = new { id };
        int count = connection.ExecuteScalar<int>(query, parameters);
        return count > 0;
    }

    public string GetAllStudents()
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM student";
            List<Student> students = connection.Query<Student>(query).ToList();

            string jsonResult = JsonConvert.SerializeObject(students);
            return jsonResult;
        }
    }
    public string addStudent(Student student)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = "INSERT INTO student (id, name, dateBirth, email, gpa, password, gender)" +
                           "VALUES (@id, @name, @dateBirth, @email, @gpa, @password, @gender);";

            Random random = new Random();
            int id = random.Next(1, 1000000);

            while (IsIdExists(connection, id))
            {
                id = random.Next(1, 1000000); 
            }

            var parameters = new
            {
                id,
                name = student.name,
                dateBirth = student.dateBirth,
                email = student.email,
                gpa = student.gpa,
                password = student.password,
                gender = student.gender
            };

            connection.Execute(query, parameters);

            string json = JsonConvert.SerializeObject(student);
            return json;

        }
    }
    public string UpdateStudent(Student student)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = "UPDATE student SET name = @name, dateBirth = @dateBirth, email = @email, gpa = @gpa, password=@password, gender = @gender WHERE id = @id;";
            var parameters = new
            {
                id = student.Id,
                name = student.name,
                dateBirth = student.dateBirth,
                email = student.email,
                gpa = student.gpa,
                password= student.password,
                gender = student.gender
               
            };

            int rowsAffected = connection.Execute(query, parameters);

            if (rowsAffected > 0) return JsonConvert.SerializeObject(student);
            return "No student with corresponding ID was found.";
            
        }
    }
    public string GetStudent(string studentName)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string queryFindByName = "SELECT * FROM student WHERE name LIKE @name;";
            string queryFindAll = "SELECT * FROM student";
            var parameters = string.IsNullOrEmpty(studentName) ? null : new { name = $"%{studentName}%" };
            string query = string.IsNullOrEmpty(studentName) ? queryFindAll : queryFindByName;

            List<Student> students = connection.Query<Student>(query, parameters).ToList();

            if (students != null) return JsonConvert.SerializeObject(students);
            return "No student with corresponding name was found";
        }
    }
    public string DeleteStudent(int studentId)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = "DELETE FROM student WHERE id = @id;";
            var parameters = new { id = studentId };

            int rowsAffected = connection.Execute(query, parameters);

            if (rowsAffected > 0) return "Delete student success";
            return "No student with corresponding ID was found to delete.";
        }
    }
    public string GetGenderCounts()
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT gender as name, COUNT(*) as value FROM student GROUP BY gender;";

            List<GenderCount> genderCounts = connection.Query<GenderCount>(query).ToList();

            if (genderCounts != null) return JsonConvert.SerializeObject(genderCounts);
            return "No gender counts found";
        }
    }
    public string GetStudentStatistics()
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = @"
            SELECT
               CASE
                WHEN GPA >= 3.5 THEN 'Excellent'
                WHEN GPA >= 3.0 AND GPA < 3.5 THEN 'Good'
                WHEN GPA < 2.0 THEN 'Average'
                WHEN GPA >= 2.0 AND GPA < 3.0 THEN 'Fair'
            END AS grade,
            COUNT(*) AS value
               FROM
                student
                GROUP BY grade
                ORDER BY CASE
                       WHEN GPA >= 3.5 THEN 1
                       WHEN GPA >= 3.0 AND GPA < 3.5 THEN 2
                       WHEN GPA < 2.0 THEN 3
                       WHEN GPA >= 2.0 AND GPA < 3.0 THEN 4
                END;
            ";

            List<StudentStatistics> studentStatistics = connection.Query<StudentStatistics>(query).ToList();

            if (studentStatistics != null) return JsonConvert.SerializeObject(studentStatistics);
            return "No student statistics found";
        }
    }



}
