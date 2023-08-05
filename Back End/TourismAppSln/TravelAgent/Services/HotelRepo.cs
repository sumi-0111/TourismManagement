//using Microsoft.EntityFrameworkCore;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;
//using TourPackage.Interfaces;
//using TourPackage.Models;

//namespace TourPackage.Services
//{
//    public class HotelRepo : IRepo<int, Hotel>
//    {
//        private readonly TourPackageContext _context;
//        private readonly ILogger<Hotel> _logger;

//        public HotelRepo(TourPackageContext context, ILogger<Hotel> logger)
//        {
//            _context = context;
//            _logger = logger;
//        }
//        public async Task<Hotel?> Add(Hotel item)
//        {
//            try
//            {
//                _context.Hotels.Add(item);
//                await _context.SaveChangesAsync();
//                return item;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.Message);
//            }
//            return null;
//        }



//        public async Task<Hotel?> Delete(int key)
//        {
//            try
//            {
//                var doc = await Get(key);
//                if (doc != null)
//                {
//                    _context.Hotels.Remove(doc);
//                    await _context.SaveChangesAsync();
//                    return doc;
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.Message);
//            }
//            return null;
//        }

//        public async Task<Hotel?> Get(int key)
//        {
//            try
//            {
//                var doc = await _context.Hotels.FirstOrDefaultAsync(i => i.HotelId == key);
//                return doc;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.Message);
//            }
//            return null;
//        }

//        public async Task<ICollection<Hotel>?> GetAll()
//        {
//            try
//            {
//                var doc = await _context.Hotels.ToListAsync();
//                if (doc.Count > 0)
//                    return doc;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.Message);
//            }
//            return null;
//        }
//        public async Task<Hotel?> Update(Hotel item)
//        {
//            try
//            {
//                var existingDoctor = await _context.Hotels.FindAsync(item.HotelId);
//                if (existingDoctor != null)
//                {
//                    existingDoctor.HotelName = item.HotelName;
//                    existingDoctor.HotelRating = item.HotelRating;
//                    existingDoctor.RoomType = item.RoomType;
//                    existingDoctor.HotelFood = item.HotelFood;


//                    await _context.SaveChangesAsync();

//                    return existingDoctor;
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.Message);
//            }
//            return null;
//        }

//    }
//}
