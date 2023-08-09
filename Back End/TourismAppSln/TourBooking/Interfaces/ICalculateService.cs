namespace TourBooking.Interfaces
{
    public interface ICalculateService
    {
        double CalculateTotalAmount(double amount, int? addTravelerCount);

    }
}
