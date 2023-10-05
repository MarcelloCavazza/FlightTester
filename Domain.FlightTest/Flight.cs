using Microsoft.Win32;
using System.Runtime.Serialization;

namespace Domain.FlightTest
{
    public class Flight
    {
        public List<Booking> seatsList { get; set; } = new List<Booking>();
        private int seatsLength;

        public Guid Id { get; }

        [Obsolete("Needed by EF")]
        public Flight()
        {
           
        }

        public Flight(int numberOfSeats)
        {
            this.seatsLength = numberOfSeats;
        }    

        public object? Book(Guid regystryId, int seatNumber, string name)
        {
            object? willOverBook = WillOverBook(), isValidSeatNumber = IsValidSeatNumber(seatNumber, regystryId);

            if (willOverBook != null) return willOverBook;
            
            if(isValidSeatNumber != null) return isValidSeatNumber;

            this.seatsList.Add(new Booking(regystryId, name, seatNumber));
            return null;
        }
        public int getNumberOfSeatsRemaining()
        {
            return this.seatsLength - this.seatsList.Count();
        }
        public object? WillOverBook()
        {
            if (this.seatsList.Count == this.seatsLength) return new OverBookingError();
            return null;
        }
        public object? ContainsARegistry(Guid registryId, string userName = "")
        {

            var result = this.seatsList.FirstOrDefault(x => x.GetId()'' == registryId);
            if (result != null)
            {
                return null;
            }
            
            
            return new SeatNotFound();
        }
        public object? CancelBook(Guid seatNumber,  string userName)
        {
            object? containsRegistry = ContainsARegistry(seatNumber, userName);
            if (containsRegistry == null)
            {
                var result = this.seatsList.FirstOrDefault(x => x.GetId() == seatNumber);
                Console.WriteLine(result);
                this.seatsList.Remove(result);
                return null;
            }

            return containsRegistry;
        }

        public object? IsValidSeatNumber(int seatNumber, Guid id)
        {
            object? containsRegistry = ContainsARegistry(id);
            if (containsRegistry == null) return new AlreadyExistsASeatRegistry();
            if (seatNumber <= 0 || seatNumber > this.seatsLength) return new InvalidSeatNumber();

            return null;

        }
    }
}