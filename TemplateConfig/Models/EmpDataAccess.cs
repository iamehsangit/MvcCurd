using Microsoft.Data.SqlClient;
using System.Data;

namespace TemplateConfig.Models
{
    public class EmpDataAccess
    {
        private readonly string _connectionString;

        public EmpDataAccess(IConfiguration configaration)
        {
            _connectionString = configaration.GetConnectionString("DefaultConnection");
        }
        public void AddEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO employee (Name,Position,Salary) VALUES (@Name, @Position, @Salary)", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM employee", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employees.Add(new Employee
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Name = rdr["Name"].ToString(),
                        Position = rdr["Position"].ToString(),
                        Salary = Convert.ToDecimal(rdr["Salary"])
                    });
                }
                con.Close();
            }

            return employees;
        }

        public Employee GetEmployeeById(int id)
        {
            Employee employee = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM employee WHERE Id = @Id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    employee = new Employee
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Name = rdr["Name"].ToString(),
                        Position = rdr["Position"].ToString(),
                        Salary = Convert.ToDecimal(rdr["Salary"])
                    };
                }
                con.Close();
            }

            return employee;
        }


        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE employee SET Name = @Name, Position = @Position, Salary = @Salary WHERE Id = @Id", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", employee.Id);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
