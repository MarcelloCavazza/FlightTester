using FluentAssertions;

namespace Application.Test
{
    public class FlightApplicationSpecifications
    {
        [Fact]
        public void Books_Flights()
        {
            const string passagensEmail = "marcello@gmail.com";
            const int numberOfSeats = 10;
            BookingService bookingService = new();
            bookingService.Book(new BookDTO(Guid.NewGuid(), passagensEmail, numberOfSeats));
            bookingService.FindBookings().Should().ContainEquivalentOf(
                new BookingRM(passagensEmail, numberOfSeats)
                );
        }

    }



    public class BookingService
    {
        public void Book(BookDTO data)
        {

        }

        public IEnumerable<BookingRM> FindBookings()
        {
            return new[]
            {
                new BookingRM("asdfa", 23)
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