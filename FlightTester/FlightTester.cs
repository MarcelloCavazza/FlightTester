using Domain.FlightTest;
using FluentAssertions;

namespace FlightTester
{
    public class FlightTester
    {
        [Fact]
        public void Booking3FlightsShouldBe0Remaining()
        {
            Flight fl = new Flight(3);
            fl.Book(1, "Marcello");
            fl.Book(2, "Marcello1");
            fl.Book(3, "Marcello2");
            fl.getNumberOfSeatsRemaining().Should().Be(0);
        }

        [Fact]
        public void ForceOverBooking()
        {
            Dictionary<int, string> dataToTest = new Dictionary<int, string>()
            {
                {1, "Marcello"},
                {2, "Feliple"},
                {3, "Pedro"},
                {4, "Lucas"},
            };
            const int flightMaxSeats = 3;
            Flight fl = new Flight(flightMaxSeats);
            foreach (var value in dataToTest)
            {
                ReturnModel result = fl.Book(value.Key, value.Value);
                if (!result.GetSucess())
                {
                    result.GetMessage().Should().Be("Over booking happening, max seats reached and a already occupied place with this seat is owned.");
                    result.GetSucess().Should().Be(false);
                }
            }
        }

        [Fact]
        public void AlreadyOccupied()
        {
            Flight fl = new Flight(3);
            fl.Book(1, "Marcello");
            fl.Book(1, "Marcello1");
        }

        [Fact]
        public void InvalidSeat()
        {
            Flight fl = new Flight(3);
            fl.Book(-1, "Marcello");
        }
    }
}