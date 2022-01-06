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
    public class TeacherController : ApiController
    {
        static string string_Connection = "Data Source=LAPTOP-HG30JHU1;Initial Catalog=CollegeDB;Integrated Security=True;Pooling=False";
        DataClasses1DataContext dataContex = new DataClasses1DataContext(string_Connection);
        //GET: api/Teacher
        public IHttpActionResult Get()
        {
            try
            {
                List<Teacher> list = dataContex.Teachers.ToList();
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

        // GET: api/Teacher/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                Teacher obj = dataContex.Teachers.First(item => item.Id == id);
                return Ok(new{obj});
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest();// not sure why i have to put that line
        }

        // POST: api/Teacher
        public IHttpActionResult Post([FromBody] Teacher value)
        {
            try
            {
                dataContex.Teachers.InsertOnSubmit(value);
                dataContex.SubmitChanges();
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

        // PUT: api/Teacher/5
        public IHttpActionResult Put(int id, [FromBody] Teacher value)
        {
            try
            {
                Teacher someObj = dataContex.Teachers.First((item) => item.Id == id);
                someObj.firstName = value.firstName;
                someObj.lastName = value.lastName;
                someObj.sectionStudy = value.sectionStudy;
                someObj.email = value.email;
                someObj.payment = value.payment;
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

        // DELETE: api/Teacher/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                dataContex.Teachers.DeleteOnSubmit(dataContex.Teachers.First(item => item.Id == id));
                dataContex.SubmitChanges();
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
