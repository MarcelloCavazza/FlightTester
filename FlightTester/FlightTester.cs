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
            ReturnModel result;
            foreach (var value in dataToTest)
            {
                result = fl.Book(value.Key, value.Value);
                result.GetMessage().Should().Be(null);
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
            ReturnModel result;
            foreach (var value in dataToTest)
            {
                result = fl.Book(value.Key, value.Value);
                bool sucess = result.GetSucess();
                if (!sucess)
                {
                    result.GetMessage().Should().Be("Over booking happening, max seats reached and a already occupied place with this seat is owned.");
                    sucess.Should().Be(false);
                }
            }
        }

        [Fact]
        public void AlreadyOccupied()
        {
            string[] dataToTest = 
            {
                "Marcello",
                "Felipe"
            };
            Flight fl = new(2);
            ReturnModel result;
            for (int i = 1; i <= dataToTest.Length; i++)
            {
                result = fl.Book(i, dataToTest[i-1]);
                bool sucess = result.GetSucess();
                if (!sucess) {
                    sucess.Should().Be(false);
                    result.GetMessage().Should().Be("This Seat is already occupied.");
                }
            }
        }

        [Fact]
        public void InvalidSeat()
        {
            //Flight fl = new(1);
            ReturnModel result =  new Flight(1).Book(-1, "Marcello");

            result.GetSucess().Should().Be(false);
            result.GetMessage().Should().Be("This Seat is a invalid position.");
        }
    }
}