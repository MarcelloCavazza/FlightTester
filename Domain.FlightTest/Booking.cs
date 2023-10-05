using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.FlightTest
{
    public class Booking
    {
        public Guid Id { get; private set; }
        private int SeatNumber { get; set; }
        private string PassengerEmail { get; set; }
        public Booking()
        {
        }
        public Booking(Guid id, string passengerEmail, int seatNumber)
        {
            this.Id = id;
            this.SeatNumber = seatNumber;
            this.PassengerEmail = passengerEmail;    
        }
        public int GetSeatNumber()
        {
            return this.SeatNumber;
        }
        public string GetPassengerEmail()
        {
            return this.PassengerEmail;
        }
    }
}
