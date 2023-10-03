using System.Runtime.Serialization;

namespace Domain.FlightTest
{
    public class Flight
    {
        private Dictionary<int, string> seatsList = new Dictionary<int, string>();
        private int seatsLength;

        public Flight(int numberOfSeats)
        {
            this.seatsLength = numberOfSeats;
        }

        public int getNumberOfSeatsRemaining()
        {
            return this.seatsLength - this.seatsList.Count();
        }

        public object? Book(int seat, string name)
        {
            object? willOverBook = WillOverBook(seat);

            if (willOverBook != null)
            {
                return willOverBook;
            }

            this.seatsList.Add(seat, name);
            return null;
        }
        public object? WillOverBook(int seat)
        {
            if (this.seatsList.Count == this.seatsLength) return new OverBookingError();
            return null;
        }
    }
}