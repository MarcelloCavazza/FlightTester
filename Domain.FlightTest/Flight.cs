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

        public ReturnModel Book(int seat, string name)
        {
            ReturnModel willOverBook = WillOverBook(seat);
            ReturnModel isValidPossibleSeat = IsValidPossibleSeat(seat);
            if (willOverBook.GetSucess() && isValidPossibleSeat.GetSucess())
            {
                this.seatsList.Add(seat, name);
            }
            else
            {
                if(!willOverBook.GetSucess()) return new ReturnModel(false, willOverBook.GetMessage());
                return new ReturnModel(false, isValidPossibleSeat.GetMessage());
            }

            return new ReturnModel(true);
        }
        public ReturnModel WillOverBook(int seat)
        {
            if (this.seatsList.Count == this.seatsLength) {
                return new ReturnModel(false, $"Over booking happening, max seats reached and a already occupied place with this seat is owned.");
            }
            return new ReturnModel(true);
        }

        public ReturnModel IsValidPossibleSeat(int seat)
        {
            ReturnModel isAlreadyOccupiedSeat = IsAlreadyOccupiedSeat(seat);
            if (!isAlreadyOccupiedSeat.GetSucess())
            {
                return new ReturnModel(false, isAlreadyOccupiedSeat.GetMessage());
            }
            ReturnModel isInValidPosition = IsInValidPosition(seat);
            if (!isInValidPosition.GetSucess())
            {
                return new ReturnModel(false, isInValidPosition.GetMessage());
            }

            return new ReturnModel(true);
        }
        public ReturnModel IsInValidPosition(int seat)
        {
            if (seat <= 0) return new ReturnModel(false, $"This Seat is a invalid position. \nInputed Seat Location: {seat}");
            return new ReturnModel(true);
        }
        public ReturnModel IsAlreadyOccupiedSeat(int seat)
        {
            if (this.seatsList.ContainsKey(seat)) return new ReturnModel(false, $"This Seat is already occupied. \nInputed Seat Location: {seat}");
            return new ReturnModel(true);
        }
    }
}