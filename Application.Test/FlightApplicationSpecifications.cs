using Data;
using Domain.FlightTest;
using FluentAssertions;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Application.Test
{
    public class FlightApplicationSpecifications
    {
        [Theory]
        [InlineData("a@b.gmail", 2)]
        [InlineData("a@c.gmail", 5)]
        public void Books_Flights(string passengerEmail, int numberOfSeats)
        {
            Entities entities = new Entities(new DbContextOptionsBuilder<Entities>().UseInMemoryDatabase("Flights").Options);
            Flight flight = new(3);
            entities.Flights.Add(flight);
            BookingService bookingService = new(entities);
            bookingService.Book(new BookDTO(flight.Id, passengerEmail, numberOfSeats));
            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(
                new BookingRM(passengerEmail, numberOfSeats)
                );
        }

    }



    public class BookingService
    {
        private Entities Entities { get; set; }
        public BookingService(Entities entities)
        {
            
        }
        public void Book(BookDTO data)
        {
            var flight = Entities.Flights.Find(data.GetFlightId());
        }

        public IEnumerable<BookingRM> FindBookings(Guid flightId)
        {
            return new[]
            {
                new BookingRM("marcello@gmail.com", 10)
            };
        }
    }

    public class BookDTO
    {
        private string PassengerEmail { get; set; }
        private int NumberOfSeats { get; set; }
        private Guid FlightId { get; set; }

        public Guid GetFlightId()
        {
            return this.FlightId;
        }
        public BookDTO(Guid flightId, string passangerEmail, int numberOfSeats)
        {
            this.NumberOfSeats = numberOfSeats;
            this.PassengerEmail = passangerEmail;
            //this.FlightId = flightId;
        }
    }

    public class BookingRM
    {
        public string PassengerEmail { get; set; }
        public int NumberOfSeats { get; set; }
        public BookingRM(string passengerEmail, int numberOfSeats)
        {
            this.NumberOfSeats = numberOfSeats;
            this.PassengerEmail = passengerEmail;
        }
    }

}