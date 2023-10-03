using Domain.FlightTest;
using FluentAssertions;
using System.Collections.Generic;

namespace FlightTester
{
    public class FlightTester
    {
        private const int flightMaxSeats = 3;

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

            foreach (var value in dataToTest)
            {
                fl.Book(value.Key, value.Value).Should().Be(null);
            }
            fl.getNumberOfSeatsRemaining().Should().Be(0);
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
                
                if (result != null)
                {
                    result.Should().BeOfType<OverBookingError>();
                }
            }
        }
    }
}