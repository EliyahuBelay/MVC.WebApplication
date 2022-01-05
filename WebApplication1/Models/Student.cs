using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Student
    {
        string firsName;
        string lastName;
        DateTime birthDate;
        string email;
        int yearOfStudy;

        public Student(string firsName, string lastName, DateTime birthDate, string email, int yearOfStudy)
        {
            this.FirsName = firsName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.Email = email;
            this.YearOfStudy = yearOfStudy;
        }

        public string FirsName { get => firsName; set => firsName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public string Email { get => email; set => email = value; }
        public int YearOfStudy { get => yearOfStudy; set => yearOfStudy = value; }

        
    }
}