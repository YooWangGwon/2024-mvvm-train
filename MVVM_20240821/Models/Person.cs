using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_20240821.Models
{
    internal class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                if(Commons.IsValidEmail(value)) throw new Exception("Invalid Email");
                else email = value; 
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get => date;
            set
            {
                var result = Commons.CalcAge(value);
                if (result > 135 || result < 0) throw new Exception("Invalid Date");
                else date = value;
            }
        }

        public string Zodiac { get { return Commons.CalcZodiac(date); } }
        public string ChnZodiac { get { return Commons.CalcChnZodiac(date); } }

        public bool IsBirthday
        {
            get
            {
                return DateTime.Now.Year == Date.Year &&
                    DateTime.Now.Month == Date.Month &&
                    DateTime.Now.Day == Date.Day;
            }
        }

        public bool IsAdult
        {
            get
            {
                return Commons.CalcAge(date) > 18;
            }
        }

        public Person(string firstName, string lastName, string email, DateTime date)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Date = date;
        }
    }
}
