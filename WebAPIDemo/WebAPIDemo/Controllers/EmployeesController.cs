using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPIDemo.Data;

namespace WebAPIDemo.Controllers
{
    public class EmployeesController : ApiController
    {
        public IEnumerable<Employee> Get()
        {
            using (EmployeeDBEntities context = new EmployeeDBEntities())
            {
                return context.Employees.ToList();
            }
        }

        public Employee Get(int id)
        {
            using (EmployeeDBEntities context = new EmployeeDBEntities())
            {
                return context.Employees.SingleOrDefault(ee => ee.Id == id);
            }
        }

        public void Post([FromBody] Employee employee)
        {
            using (EmployeeDBEntities context = new EmployeeDBEntities())
            {
                context.Employees.Add(employee);
                context.SaveChanges();
            }
        }
    }
}
