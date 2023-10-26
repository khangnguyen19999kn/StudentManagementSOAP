using Dapper;
using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Connection;


public class DatabaseHelper
{
    private string _connectionString;

    public DatabaseHelper(string connectionString)
    {
        _connectionString = connectionString;
    }

    public string GetAllStudents()
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM student";
            List<Student> students = connection.Query<Student>(query).ToList();

            // Chuyển đổi danh sách sinh viên sang định dạng JSON
            string jsonResult = JsonConvert.SerializeObject(students);
            return jsonResult;
        }
    }
    public string addStudent(Student student)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = "INSERT INTO student (id,name, dateBirth, email, gpa, password) " +
                             "VALUES (@id, @name, @dateBirth, @email, @gpa, @password);";
            var parameters = new
            {
                id = student.Id,
                name = student.name,
                dateBirth = student.dateBirth,
                email = student.email,
                gpa = student.gpa,
                password = student.password
            };
            connection.Execute(query, parameters);
            // Thực hiện truy vấn INSERT
            string json = JsonConvert.SerializeObject(student);
            return json;

        }
    }
    public string UpdateStudent(Student student)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = "UPDATE student SET name = @name, dateBirth = @dateBirth, email = @email, gpa = @gpa, password = @password WHERE id = @id;";
            var parameters = new
            {
                id = student.Id,
                name = student.name,
                dateBirth = student.dateBirth,
                email = student.email,
                gpa = student.gpa,
                password = student.password
            };

            // Thực hiện truy vấn UPDATE
            int rowsAffected = connection.Execute(query, parameters);

            if (rowsAffected > 0)
            {
                // Trả về dữ liệu đã cập nhật dưới dạng JSON nếu có bản ghi được cập nhật
                string json = JsonConvert.SerializeObject(student);
                return json;
            }
            else
            {
                return "Không tìm thấy sinh viên có ID tương ứng.";
            }
        }
    }
    public string GetStudent(int studentId)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM student WHERE id = @id;";
            var parameters = new { id = studentId };

            // Thực hiện truy vấn SELECT để lấy thông tin sinh viên theo ID
            Student student = connection.QueryFirstOrDefault<Student>(query, parameters);

            if (student != null)
            {
                // Nếu tìm thấy sinh viên có ID tương ứng, trả về thông tin dưới dạng JSON
                string json = JsonConvert.SerializeObject(student);
                return json;
            }
            else
            {
                // Nếu không tìm thấy sinh viên, trả về một thông báo hoặc chuỗi JSON trống
                return "Không tìm thấy sinh viên có ID tương ứng.";
            }
        }
    }
    public string DeleteStudent(int studentId)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = "DELETE FROM student WHERE id = @id;";
            var parameters = new { id = studentId };

            // Thực hiện truy vấn DELETE để xóa sinh viên theo ID
            int rowsAffected = connection.Execute(query, parameters);

            if (rowsAffected > 0)
            {
                // Nếu có bản ghi bị xóa, trả về thông báo xóa thành công hoặc chuỗi JSON trống
                return "Xóa sinh viên thành công.";
            }
            else
            {
                // Nếu không có bản ghi nào bị xóa, trả về thông báo rằng không tìm thấy sinh viên có ID tương ứng
                return "Không tìm thấy sinh viên có ID tương ứng để xóa.";
            }
        }
    }




}
