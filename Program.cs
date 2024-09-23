namespace ConsoleApp7
{
    using Dapper;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Data.SqlClient;

    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Experience { get; set; }
    }

    public class TeacherRepository
    {
        private readonly string _connectionString;

        public TeacherRepository(string connectionString)
        {
            _connectionString = "Data Source = ALI\\SQLEXPRESS; Initial Catalog = DBB; Trusted_Connection = True; Trust Server Certificate = True;";
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public void Insert(Teacher teacher)
        {
            using (var connection = CreateConnection())
            {
                string query = "INSERT INTO Teachers (FirstName, LastName, Experience) VALUES (@FirstName, @LastName, @Experience)";
                connection.Execute(query, new { teacher.FirstName, teacher.LastName, teacher.Experience });
            }
        }

        public Teacher GetById(int id)
        {
            using (var connection = CreateConnection())
            {
                string query = "SELECT * FROM Teachers WHERE Id = @Id";
                return connection.QueryFirstOrDefault<Teacher>(query, new { Id = id });
            }
        }

        public IEnumerable<Teacher> GetAll()
        {
            using (var connection = CreateConnection())
            {
                string query = "SELECT * FROM Teachers";
                return connection.Query<Teacher>(query).ToList();
            }
        }

        public void Delete(int id)
        {
            using (var connection = CreateConnection())
            {
                string query = "DELETE FROM Teachers WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }
        }

        public void Update(Teacher teacher)
        {
            using (var connection = CreateConnection())
            {
                string query = "UPDATE Teachers SET FirstName = @FirstName, LastName = @LastName, Experience = @Experience WHERE Id = @Id";
                connection.Execute(query, new { teacher.FirstName, teacher.LastName, teacher.Experience, teacher.Id });
            }
        }
    }






}
