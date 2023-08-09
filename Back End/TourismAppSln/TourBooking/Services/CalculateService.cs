using TourBooking.Interfaces;

namespace TourBooking.Services
{
    public class CalculateService:ICalculateService
    {
        public double CalculateTotalAmount(double amount, int? addTravelerCount)
        {
            if (addTravelerCount.HasValue && addTravelerCount.Value > 0)
            {
                return amount * addTravelerCount.Value;
            }

            return amount;
        }
    }
}
