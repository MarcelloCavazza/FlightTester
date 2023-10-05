using Domain.FlightTest;
using FluentAssertions;
using System.Collections.Generic;

namespace FlightTester
{
    public class FlightTester
    {
        protected const int flightMaxSeats = 3;

        [Fact]
        public void SucessfulBooking()
        {
            Flight fl = new(flightMaxSeats);
            object? error = fl.Book(Guid.NewGuid(), 1, "Marcello");
            error.Should().BeNull();
        }

        [Theory]
        [InlineData(1, "Marcello", 1)]
        [InlineData(2, "Marcos",3)]
        [InlineData(3, "Felipe",3)]
        public void VerifyIfBookWasCreated(int seatNumber, string seatName, int maxSeat)
        {
            Flight fl = new (maxSeat);
            Guid seatId = Guid.NewGuid();
            fl.Book(seatId, seatNumber, seatName).Should().BeNull();
            fl.ContainsARegistry(seatId, seatName).Should().BeNull();
        }

        [Theory]
        [InlineData(2, "marcello", 1)]
        [InlineData(5, "marcos", 3)]
        [InlineData(-1, "Felipe", 3)]
        public void IncorrectSeatNumber(int seatNumber, string seatName, int maxSeat)
        {
            Flight fl = new (maxSeat);
            Guid id = Guid.NewGuid();
            fl.Book(id, seatNumber, seatName).Should().BeOfType<InvalidSeatNumber>();
        }

        [Theory]
        [InlineData(3, new string[]{"Marcello", "Pedro", "Felipe"})]
        [InlineData(4, new string[]{"Marcello", "Pedro", "Felipe"})]
        [InlineData(7, new string[]{"Marcello", "Pedro", "Felipe"})]
        public void BookingReducesNumberOfSeats(int seatMaxCapacity, string[] dataToBook)
        {
            Flight fl = new(seatMaxCapacity);
            for(int i = 0; i< dataToBook.Length; i++)
            {
                fl.Book(Guid.NewGuid(), i+1, dataToBook[i]);
            }
            int seatDiff = seatMaxCapacity - dataToBook.Count();
            seatDiff.Should().BeGreaterThanOrEqualTo(0);
            fl.getNumberOfSeatsRemaining().Should().Be(seatDiff);
        }

        [Fact]
        public void ForceOverBooking()
        {
            Dictionary<int, string> dataToTest = new()
            {
                {1, "Marcello"},
                {2, "Felipe"},
                {3, "Pedro"},
                {4, "Lucas"},
            };
            Flight fl = new(flightMaxSeats);

            foreach (var value in dataToTest)
            {
                object? result = fl.Book(Guid.NewGuid(), value.Key, value.Value);
                
                result?.Should().BeOfType<OverBookingError>();
            }
        }

        [Fact]
        public void CancelAlreadyMadeBooking()
        {
            int seatNumber = 1, maxSeats = 3;
            string userName = "Marcello";
            Flight flight = new(maxSeats);
            Guid id = Guid.NewGuid();
            flight.Book(id, seatNumber, userName).Should().BeNull();
            flight.CancelBook(id, userName).Should().BeNull();
            flight.getNumberOfSeatsRemaining().Should().Be(maxSeats);
        }
    }
}