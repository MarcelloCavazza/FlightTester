using FluentAssertions;

namespace Application.Test
{
    public class FlightApplicationSpecifications
    {
        [Fact]
        public void Books_Flights()
        {
            BookingService bookingService = new();
            bookingService.Book(new BookDTO());
            bookingService.FindBookings().Should().ContainEquivalentOf(
                new BookingRM()
                );
        }

    }

    public class BookDTO
    {

    }

    public class BookingService
    {
        public void Book(BookDTO data)
        {

        }

        public IEnumerable<BookingRM> FindBookings()
        {

        }
    }

    public class BookingRM
    {

    }
}