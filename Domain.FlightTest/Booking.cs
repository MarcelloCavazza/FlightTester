using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.FlightTest
{
    public class Booking
    {
        public int Id { get; set; }
        public string PassengerEmail { get; set; }
        public Booking(int id, string passengerEmail)
        {
            this.Id = id;
            this.PassengerEmail = passengerEmail;    
        }
    }
}
