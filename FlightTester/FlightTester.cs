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
            object? error = fl.Book(1, "Marcello");
            error.Should().BeNull();
        }

        [Theory]
        [InlineData(1, "Marcello", 1)]
        [InlineData(2, "Marcos",3)]
        [InlineData(3, "Felipe",3)]
        public void ReallyCreatedABooking(int seatNumber, string seatName, int maxSeat)
        {
            Flight fl = new Flight(maxSeat);

            fl.Book(seatNumber, seatName);

            fl.ContainsARegistry(seatNumber, seatName).Should().BeNull();
        }

        [Theory]
        [InlineData(2, "marcello", 1)]
        [InlineData(5, "marcos", 3)]
        [InlineData(-1, "Felipe", 3)]
        public void IncorrectSeatNumber(int seatNumber, string seatName, int maxSeat)
        {
            Flight fl = new Flight(maxSeat);

            fl.Book(seatNumber, seatName).Should().BeOfType<InvalidSeatNumber>();
        }

        [Theory]
        [InlineData(3, new string[]{"Marcello", "Pedro", "Felipe"})]
        [InlineData(4, new string[]{"Marcello", "Pedro", "Felipe"})]
        [InlineData(7, new string[]{"Marcello", "Pedro", "Felipe"})]
        //[InlineData(2, new string[]{"Marcello", "Pedro", "Felipe"})] assim o teste quebra
        public void BookingReducesNumberOfSeats(int seatMaxCapacity, string[] dataToBook)
        {
            Flight fl = new(seatMaxCapacity);
            for(int i = 0; i< dataToBook.Length; i++)
            {
                fl.Book(i+1, dataToBook[i]);
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
                object? result = fl.Book(value.Key, value.Value);
                
                result?.Should().BeOfType<OverBookingError>();
            }
        }

        [Fact]
        public void CancelAlreadyMadeBooking()
        {
            int seatNumber = 1, maxSeats = 3;
            string userName = "Marcello";

            Flight flight = new(maxSeats);

            flight.Book(seatNumber, userName).Should().BeNull();
            flight.ContainsARegistry(seatNumber, userName).Should().BeNull();
            flight.CancelBook(seatNumber, userName).Should().BeNull();
        }
    }
}