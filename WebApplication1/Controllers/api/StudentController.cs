using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers.api
{
    public class StudentController : ApiController
    {

        static string stringConnection = "Data Source=LAPTOP-HG30JHU1;Initial Catalog=CollegeDB;Integrated Security=True;Pooling=False";
        DataClasses1DataContext dataContext = new DataClasses1DataContext(stringConnection);
        // GET: api/Student
        public IHttpActionResult Get()
        {
            try
            {
                List<Student> list = dataContext.Students.ToList();
                return Ok(new { list });
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Student/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                Student someObj = dataContext.Students.First(item => item.Id == id);
                return Ok(new { someObj });
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST: api/Student
        public IHttpActionResult Post([FromBody] Student value)
        {

            try
            {
                dataContext.Students.InsertOnSubmit(value);
                dataContext.SubmitChanges();
                return Get();
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Student/5
        public IHttpActionResult Put(int id, [FromBody] Student value)
        {
            try
            {
                Student obj = dataContext.Students.First((item)=> item.Id == id);
                obj.firstName = value.firstName;
                obj.lastName = value.lastName;
                obj.birthDate = value.birthDate;
                obj.email = value.email;
                obj.yearOfStudy = value.yearOfStudy;
                return Get();
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //DELETE: api/Student/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                dataContext.Students.DeleteOnSubmit(dataContext.Students.First((item) => item.Id == id));
                dataContext.SubmitChanges();
                return Get();
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

