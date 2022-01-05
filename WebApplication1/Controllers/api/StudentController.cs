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
        
        string stringConnection = "Data Source=LAPTOP-HG30JHU1;Initial Catalog=CollegeDB;Integrated Security=True;Pooling=False";
        // GET: api/Student
        public IHttpActionResult Get()
        {
             List<Student> studentList = new List<Student>();
            try
            {
                using (SqlConnection connection = new SqlConnection(stringConnection))
                {
                    connection.Open();
                    string query = @"SELECT * FROM Student";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader dataFromDB = cmd.ExecuteReader();
                    if (dataFromDB.HasRows)
                    {
                        while (dataFromDB.Read())
                        {
                            studentList.Add(new Student(dataFromDB.GetString(1), dataFromDB.GetString(2), dataFromDB.GetDateTime(3), dataFromDB.GetString(4), dataFromDB.GetInt32(5)));
                        }
                        connection.Close();
                        return Ok(new { studentList });
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

        // GET: api/Student/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(stringConnection))
                {
                    connection.Open();
                    string query = $@"SELECT * FROM Student WHERE Id = {id}";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader dataFromDB = cmd.ExecuteReader();
                    if (dataFromDB.HasRows)
                    {
                        while (dataFromDB.Read())
                        {
                             Student obj = new Student(dataFromDB.GetString(1), dataFromDB.GetString(2), dataFromDB.GetDateTime(3), dataFromDB.GetString(4), dataFromDB.GetInt32(5));
                            connection.Close();
                            return Ok(  obj );
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
            return Ok();
        }

        // POST: api/Student
        public IHttpActionResult Post([FromBody]Student value)
        {
            List<Student> studentList = new List<Student>();
            try
            {
                using(SqlConnection connection = new SqlConnection(stringConnection))
                {
                    connection.Open();
                    string query = $@"INSERT INTO Student (firstName,lastName,birthDate,email,yearOfStudy) VALUES ('{value.FirsName}','{value.LastName}','{value.BirthDate}','{value.Email}','{value.YearOfStudy}')";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    int rowEffected = cmd.ExecuteNonQuery();
                    
                    if (rowEffected == 0)
                    {
                        string empty = "nothing added to data base";
                        connection.Close();
                        return Ok(new {empty});
                    }
                    else if (rowEffected == 1)
                    {
                        string query2 = @"SELECT * FROM Student";
                        SqlCommand command = new SqlCommand(query2, connection);
                        SqlDataReader datafromDB = command.ExecuteReader();

                        if (datafromDB.HasRows)
                        {
                            while (datafromDB.Read())
                            {
                                studentList.Add(new Student(datafromDB.GetString(1), datafromDB.GetString(2), datafromDB.GetDateTime(3), datafromDB.GetString(4), datafromDB.GetInt32(5)));
                            }
                            connection.Close();
                            return Ok(new { studentList });
                        }

                        else
                        {
                            string empty = "there is no data in data base although your add succeed";
                            connection.Close();
                            return Ok(new { empty });
                        }
                    }
                    else
                    {
                        string problem = "too many rows effected ";
                        connection.Close();
                        return Ok(new{ problem});
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

        // PUT: api/Student/5
        public IHttpActionResult Put(int id, [FromBody] Student value)
        {
            List<Student> studentList = new List<Student>();
            try
            {
                using(SqlConnection connection = new SqlConnection(stringConnection))
                {
                    connection.Open();
                    string query = $@"UPDATE Student SET firstName = '{value.FirsName}',lastName = '{value.LastName}',birthDate = '{value.BirthDate}',email = '{value.Email}',yearOfStudy = {value.YearOfStudy} WHERE Id = {id}";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    int rowEffected = cmd.ExecuteNonQuery();
                    if (rowEffected == 0)
                    {
                        string empty = "nothing eddited to data base";
                        connection.Close();
                        return Ok(new { empty });
                    }
                    else if (rowEffected == 1)
                    {
                        string query2 = @"SELECT * FROM Student";
                        SqlCommand command = new SqlCommand(query2, connection);
                        SqlDataReader datafromDB = command.ExecuteReader();

                        if (datafromDB.HasRows)
                        {
                            while (datafromDB.Read())
                            {
                                studentList.Add(new Student(datafromDB.GetString(1), datafromDB.GetString(2), datafromDB.GetDateTime(3), datafromDB.GetString(4), datafromDB.GetInt32(5)));
                            }
                            connection.Close();
                            return Ok(new { studentList });
                        }

                        else
                        {
                            string empty = "there is no data in data base although your add succeed";
                            connection.Close();
                            return Ok(new { empty });
                        }
                    }
                    else
                    {
                        string problem = "too many rows effected nothing changed";
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

        //DELETE: api/Student/5
        public IHttpActionResult Delete(int id)
        {
            List<Student> studentList = new List<Student>();
            try
            {
                using(SqlConnection connection = new SqlConnection(stringConnection))
                {
                    connection.Open();
                    string query = $@"DELETE FROM Student WHERE Id = {id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    int rowEffected = command.ExecuteNonQuery();
                    if (rowEffected == 0)
                    {
                        string empty = "nothing deleted from data base";
                        connection.Close();
                        return Ok(new { empty });
                    }
                    else if (rowEffected == 1)
                    {
                        string query2 = @"SELECT * FROM Student";
                        SqlCommand cmd = new SqlCommand(query2, connection);
                        SqlDataReader datafromDB = cmd.ExecuteReader();

                        if (datafromDB.HasRows)
                        {
                            while (datafromDB.Read())
                            {
                                studentList.Add(new Student(datafromDB.GetString(1), datafromDB.GetString(2), datafromDB.GetDateTime(3), datafromDB.GetString(4), datafromDB.GetInt32(5)));
                            }
                            connection.Close();
                            return Ok(new { studentList });
                        }

                        else
                        {
                            string empty = "there is no data in data base although your delete succeed";
                            connection.Close();
                            return Ok(new { empty });
                        }
                    }
                    else
                    {
                        string problem = "too many rows effected . nothing changed!";
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
    }
}
