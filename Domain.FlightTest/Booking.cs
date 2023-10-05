using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.FlightTest
{
    public class Booking
    {
        private Guid Id { get; set; }
        private int seatNumber { get; set; }
        private string PassengerEmail { get; set; }
        public Booking(Guid id, string passengerEmail, int SeatNumber)
        {
            this.Id = id;
            this.seatNumber = SeatNumber;
            this.PassengerEmail = passengerEmail;    
        }
        public Guid GetId()
        {
            return this.Id;
        }
        public int GetSeatNumber()
        {
            return this.seatNumber;
        }
        public string GetPassengerEmail()
        {
            return this.PassengerEmail;
        }
    }
}
