using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using MVCCRUD2.Models;


namespace MVC_WebApplication1.Controllers
{
    public class Employee_Controller : Controller
    {
        public IConfiguration _configuration { get; }
        SqlConnection con;
        public Employee_Controller(IConfiguration configuration)
        {
            _configuration = configuration;
            con = new SqlConnection(_configuration.GetConnectionString("DataBase"));
        }
       
        // GET: Employee_Controller
        public ActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Employee";
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                employees.Add(new Employee
                {
                    EmpId = Convert.ToInt32(sdr["EmpId"]),
                    EmpName = Convert.ToString(sdr["EmpName"]),
                    EmailId = Convert.ToString(sdr["EmailId"])
                });
            }
            return View(employees);
        }
       
        // GET: Employee_Controller/Details/5
        [HttpGet]
        [ValidateAntiForgeryToken]
        public Employee Detail(int id)
        {
            Employee employee = new Employee();
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Employee Where(EmpId=" + id + ");";
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                employee = (new Employee
                {
                    EmpId = Convert.ToInt32(sdr["EmpId"]),
                    EmpName = sdr["EmpName"].ToString(),
                    EmailId = sdr["EmailId"].ToString(),
                });
                sdr.Close();
            }
            finally
            {
                con.Close();
            }
            return employee;
        }
        public ActionResult Details(int id)
        {
            return View(Detail(id));
        }
       
        // GET: Employee_Controller/Create
        public ActionResult Create()
        {
            return View();
        }
       
        // POST: Employee_Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into Employee(EmpId,EmpName,EmailId) values(" + employee.EmpId + ",'" + employee.EmpName + "','" + employee.EmailId + "')";
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }
       
        // GET: Employee_Controller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        
        // POST: Employee_Controller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee employee)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Update Employee Set EmpName='" + employee.EmpName + "',EmailId= '" + employee.EmailId + "' Where(EmpId= " + id + ");";
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }
       
        // GET: Employee_Controller/Delete/5
        public ActionResult Delete(int id)
        {
            return View(Detail(id));
        }
       
        // POST: Employee_Controller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Employee employee)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete from Employee Where(EmpId =" + id + ");";
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }
    }
}




