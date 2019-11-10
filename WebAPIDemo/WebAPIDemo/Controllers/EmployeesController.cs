using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
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

        public HttpResponseMessage Get(int id)
        {
            using (EmployeeDBEntities context = new EmployeeDBEntities())
            {
                var entity = context.Employees.SingleOrDefault(ee => ee.Id == id);

                if (entity != null)
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, $"Employee with id = {id.ToString(new CultureInfo("en-US"))} not found.");
                }
            }
        }

        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    if (employee == null)
                    {
                        throw new ArgumentNullException(nameof(employee));
                    }

                    context.Employees.Add(employee);
                    context.SaveChanges();

                    var message = Request.CreateResponse(System.Net.HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.Id.ToString(new CultureInfo("en-US")));

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
