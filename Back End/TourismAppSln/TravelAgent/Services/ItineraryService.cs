using TourPackage.Interfaces;
using TourPackage.Models;

namespace TourPackage.Services
{
    public class ItineraryService : IItineraryService
    {
        private readonly IRepo<int, Itinerary> _itineraryRepo;

        public ItineraryService(IRepo<int, Itinerary> itineraryRepo)
        {
            _itineraryRepo = itineraryRepo;
        }
        public async Task<Itinerary?> AddItinerary(Itinerary itinerary)
        {
            try
            {
                return await _itineraryRepo.Add(itinerary);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding itinerary: " + ex.Message);
                return null;
            }
        }


        public async Task<Itinerary?> DeleteItinerary(int id)
        {
            try
            {
                var itinerary = await _itineraryRepo.Get(id);
                if (itinerary != null)
                {
                    return await _itineraryRepo.Delete(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting itinerary: " + ex.Message);
            }
            return null;
        }


        public async Task<ICollection<Itinerary>?> GetAllItineraries()
        {
            try
            {
                var itineraries = await _itineraryRepo.GetAll();
                return itineraries;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting all itineraries: " + ex.Message);
                return null;
            }
        }

        public async Task<Itinerary?> GetItineraryById(int id)
        {
            try
            {
                return await _itineraryRepo.Get(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting itinerary: " + ex.Message);
                return null;
            }
        }

        public async Task<Itinerary?> UpdateItinerary(Itinerary itinerary)
        {
            try
            {
                return await _itineraryRepo.Update(itinerary);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating itinerary: " + ex.Message);
                return null;
            }
        }

    }
}