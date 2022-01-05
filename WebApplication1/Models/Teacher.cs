using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Teacher
    {
        string firstName;
        string lastName;
        string sectionStudy;
        string email;
        int payment;

        public Teacher(string firstName, string lastName, string sectionStudy, string email, int payment)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.SectionStudy = sectionStudy;
            this.Email = email;
            this.Payment = payment;
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string SectionStudy { get => sectionStudy; set => sectionStudy = value; }
        public string Email { get => email; set => email = value; }
        public int Payment { get => payment; set => payment = value; }
    }
}