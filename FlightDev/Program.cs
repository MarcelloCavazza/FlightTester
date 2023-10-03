//public void Booking3FlightsShouldBe0Remaining        {
using Domain.FlightTest;

            Flight fl = new Flight(3);
            fl.Book(1, "Marcello");
            fl.Book(2, "Marcello1");
            fl.Book(3, "Marcello2");
            fl.getNumberOfSeatsRemaining();
        //public void ForceOverBooking()
            Flight fl2 = new Flight(3);
            fl2.Book(1, "Marcello");
            fl2.Book(2, "Marcello1");
            fl2.Book(3, "Marcello2");
            ReturnModel result = fl2.Book(4, "Marcello2");

Console.WriteLine("Result:\nSucess:"+result.GetSucess());
if (!result.GetSucess())
{
    Console.WriteLine("\nFrase:"+result.GetMessage());
}
//public void AlreadyOccupied()

Flight fl3 = new Flight(3);
            fl3.Book(1, "Marcello");
            fl3.Book(1, "Marcello1");

//        public void InvalidSeat()

            Flight fl4 = new Flight(3);
            fl4.Book(-1, "Marcello");


Console.ReadLine();