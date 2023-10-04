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

        public object? Book(int seat, string name)
        {
            object? willOverBook = WillOverBook(), isValidSeatNumber = IsValidSeatNumber(seat);

            if (willOverBook != null) return willOverBook;
            
            if(isValidSeatNumber != null) return isValidSeatNumber;

            this.seatsList.Add(seat, name);
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
        public object? ContainsARegistry(int registryId, string userName = "")
        {
            bool userNameNull = true;
            if (!string.IsNullOrEmpty(userName))
            {
                userNameNull = false;
            }
            if (userNameNull)
            {
                if (this.seatsList.ContainsKey(registryId))
                {
                    return null;
                }
            }
            else
            {
                if (this.seatsList.ContainsKey(registryId) && this.seatsList[registryId] == userName)
                {
                    return null;
                }
            }
            
            return new SeatNotFound();
        }
        public object? CancelBook(int seatNumber,  string userName)
        {
            object? containsRegistry = ContainsARegistry(seatNumber, userName);
            if (containsRegistry == null)
            {
                this.seatsList.Remove(seatNumber);
                return null;
            }

            return containsRegistry;
        }

        public object? IsValidSeatNumber(int seatNumber)
        {
            object? containsRegistry = ContainsARegistry(seatNumber);
            if (containsRegistry == null) return new AlreadyExistsASeatRegistry();
            if (seatNumber <= 0 || seatNumber > this.seatsLength) return new InvalidSeatNumber();

            return null;

        }
    }
}