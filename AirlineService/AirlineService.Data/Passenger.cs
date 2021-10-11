using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace AirlineService.Data
{
    public class Passenger
    {
        public int PassengerID { get; set; }

        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; } // Name

        [Display(Name = "Age")]
        public int Age { get; set; } // Age

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } // Email

        [Display(Name = "Bookings")]
        public List<string> Bookings { get; set; } // TODO

        public Passenger() { }

        public Passenger(string name, string email, int age)
        {
            this.Name = name;
            this.Email = email;
            this.Age = age;
        }

        public override string ToString()
        {
            return $"Passenger: {this.PassengerID} Name: {this.Name} Age: {this.Age} Email: {this.Email}";
        }
    }
}
