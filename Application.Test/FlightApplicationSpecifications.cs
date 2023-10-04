using Data;
using Domain.FlightTest;
using FluentAssertions;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Application.Test
{
    public class FlightApplicationSpecifications
    {
        [Fact]
        public void Books_Flights()
        {
            const string passagensEmail = "marcello@gmail.com";
            const int numberOfSeats = 10;
            Entities entities = new Entities(new DbContextOptionsBuilder<Entities>().UseInMemoryDatabase("Flights").Options);
            Flight flight = new(3);
            entities.Flights.Add(flight);
            BookingService bookingService = new(entities);
            bookingService.Book(new BookDTO(flight.Id, passagensEmail, numberOfSeats));
            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(
                new BookingRM(passagensEmail, numberOfSeats)
                );
        }

    }



    public class BookingService
    {
        public BookingService(Entities entities)
        {
            
        }
        public void Book(BookDTO data)
        {

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