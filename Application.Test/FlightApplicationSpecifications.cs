using Data;
using Domain.FlightTest;
using FluentAssertions;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.Test
{
    public class FlightApplicationSpecifications
    {
        [Theory]
        [InlineData("a@b.gmail", 2)]
        [InlineData("a@c.gmail", 5)]
        public void Books_Flights(string passengerEmail, int numberOfSeats)
        {
            Guid Id = Guid.NewGuid();
            Entities entities = new Entities(new DbContextOptionsBuilder<Entities>().UseInMemoryDatabase("Flights").Options);
            Flight flight = new Flight(3);
            entities.Flights.Add(flight);
            BookingService bookingService = new BookingService(entities);
            bookingService.Book(new BookDTO(flight.Id, passengerEmail, numberOfSeats));
            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(
                new BookingRM(passengerEmail, numberOfSeats, Id)
                );
        }

    }



    public class BookingService
    {
        private Entities Entities { get; set; }
        public BookingService(Entities entities)
        {
            Entities = entities;
        }
        public void Book(BookDTO data)
        {
            var flight = this.Entities.Flights.Find(data.GetFlightId());
            flight.Book(data.GetFlightId(), data.GetNumberOfSeats(), data.GetPassengerEmail());
            this.Entities.SaveChanges();
        }

        public IEnumerable<Booking> FindBookings(Guid flightId)
        {
            var temp = this.Entities.Flights.Find(flightId).GetSeatsList();
            var temp2 = temp.Select(x => new Booking(
                    x.Id,
                    x.GetPassengerEmail(),
                    x.GetSeatNumber()
                ));
            return temp2;
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

        public string GetPassengerEmail()
        {
            return this.PassengerEmail;
        }

        public int GetNumberOfSeats()
        {
            return this.NumberOfSeats;
        }
        public BookDTO(Guid flightId, string passangerEmail, int numberOfSeats)
        {
            this.NumberOfSeats = numberOfSeats;
            this.PassengerEmail = passangerEmail;
            this.FlightId = flightId;
        }
    }

    public class BookingRM
    {
        public Guid Id { get; set; }
        public string PassengerEmail { get; set; }
        public int NumberOfSeats { get; set; }
        public BookingRM(string passengerEmail, int numberOfSeats, Guid id)
        {
            this.NumberOfSeats = numberOfSeats;
            this.PassengerEmail = passengerEmail;
            this.Id = id;
        }
    }

}