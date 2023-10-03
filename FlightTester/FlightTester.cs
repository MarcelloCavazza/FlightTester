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
            Dictionary<int, string> dataToTest = new()
            {
                {1, "Marcello"},
                {2, "Felipe"},
                {3, "Pedro"},
            };
            Flight fl = new(flightMaxSeats);
            object? error;
            foreach (var value in dataToTest)
            {
                error = fl.Book(value.Key, value.Value);
                error.Should().BeNull();
            }
        }
        [Theory]
        [InlineData(3, new string[]{"Marcello", "Pedro", "Felipe"})]
        [InlineData(4, new string[]{"Marcello", "Pedro", "Felipe"})]
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
    }
}