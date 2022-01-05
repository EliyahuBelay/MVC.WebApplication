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
        string string_Connection = "Data Source=LAPTOP-HG30JHU1;Initial Catalog=CollegeDB;Integrated Security=True;Pooling=False";

        //GET: api/Teacher
        public IHttpActionResult Get()
        {
            List<Teacher> teacherList = new List<Teacher>();
            try
            {
                using (SqlConnection connection = new SqlConnection(string_Connection))
                {
                    connection.Open();
                    string query = "SELECT * FROM Teacher";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader dataFromDB = cmd.ExecuteReader();
                    if (dataFromDB.HasRows)
                    {
                        while (dataFromDB.Read())
                        {
                            teacherList.Add(new Teacher(dataFromDB.GetString(1), dataFromDB.GetString(2), dataFromDB.GetString(3), dataFromDB.GetString(4), dataFromDB.GetInt32(5)));
                        }
                        connection.Close();
                        return Ok(new { teacherList });
                    }
                    else
                    {
                        string empty = "there is no data in data base";
                        connection.Close();
                        return Ok(new { empty });
                    }
                }
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
                using (SqlConnection connection = new SqlConnection(string_Connection))
                {
                    connection.Open();
                    string query = $@"SELECT * FROM Teacher WHERE Id = {id}";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader dataFromDB = cmd.ExecuteReader();
                    if (dataFromDB.HasRows)
                    {
                        while (dataFromDB.Read())
                        {
                            Teacher obj = new Teacher(dataFromDB.GetString(1), dataFromDB.GetString(2), dataFromDB.GetString(3), dataFromDB.GetString(4), dataFromDB.GetInt32(5));
                            connection.Close();
                            return Ok(new { obj });
                        }
                    }
                    else
                    {
                        string empty = "there is no such as id in data base";
                        connection.Close();
                        return Ok(new { empty });
                    }
                }
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
                using (SqlConnection connection = new SqlConnection(string_Connection))
                {
                    connection.Open();
                    string query = $@"INSERT INTO Teacher (firstName,lastName,sectionStudy,email,payment) VALUES ('{value.FirstName}','{value.LastName}','{value.SectionStudy}','{value.Email}','{value.Payment}')";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    int rowEffected = cmd.ExecuteNonQuery();
                    
                    if (rowEffected == 1)
                    {
                       return Get();
                    }
                    else if(rowEffected == 0)
                    {
                        string empty = "nothing added to data base";
                        connection.Close();
                        return Ok(new { empty });
                    }
                    else
                    {
                        string problem = "too many rows effected ";
                        connection.Close();
                        return Ok(new { problem });
                    }
                }
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
                using(SqlConnection connection = new SqlConnection(string_Connection))
                {
                    connection.Open();
                    string query = $@"UPDATE Teacher SET firstName = '{value.FirstName}', lastName = '{value.LastName}', sectionStudy = '{value.SectionStudy}', email = '{value.Email}', payment = { value.Payment } WHERE Id = { id }";
                    SqlCommand cmd = new SqlCommand(query,connection);
                    int rowEffected = cmd.ExecuteNonQuery();
                    if (rowEffected == 1)
                    {
                        return Get();
                    }
                    else if(rowEffected == 0)
                    {
                        string empty = "nothing added to data base";
                        connection.Close();
                        return Ok(new { empty });
                    }
                    else
                    {
                        string problem = "too many rows effected .nothing changed!";
                        connection.Close();
                        return Ok(new { problem });
                    }
                }
            }
            catch(SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Teacher/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(string_Connection))
                {
                    connection.Open();
                    string query = $@"DELETE FROM Teacher WHERE Id = {id}";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    int rowEffected = cmd.ExecuteNonQuery();
                    if (rowEffected == 1)
                    {
                        return Get();
                    }
                    else if (rowEffected == 0)
                    {
                        string empty = "nothing delete from data base";
                        connection.Close();
                        return Ok(new { empty });
                    }
                    else
                    {
                        string problem = "too many rows effected ";
                        connection.Close();
                        return Ok(new { problem });
                    }
                }
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
