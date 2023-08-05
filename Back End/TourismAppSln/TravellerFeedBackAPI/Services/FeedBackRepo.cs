using Microsoft.EntityFrameworkCore;
using TravellerFeedBackAPI.Interface;
using TravellerFeedBackAPI.Models;

namespace TravellerFeedBackAPI.Services
{
    public class FeedBackRepo : IRepo<int, UserFeedBack>
    {
        private FeedBackContext _context;
        private ILogger<UserFeedBack> _logger;

        public FeedBackRepo(FeedBackContext context, ILogger<UserFeedBack> logger)
        {
            _context = context;
            _logger=logger;
        }
        public async Task<UserFeedBack?> Add(UserFeedBack item)
        {
                try
                {
                    _context.UserFeedBacks.Add(item);
                    await _context.SaveChangesAsync();
                    return item;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                return null;
            
        }

        public async Task<UserFeedBack?> Delete(int key)
        {
            try
            {
                var doc = await Get(key);
                if (doc != null)
                {
                    _context.UserFeedBacks.Remove(doc);
                    await _context.SaveChangesAsync();
                    return doc;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<UserFeedBack?> Get(int key)
        {
            try
            {
                var doc = await _context.UserFeedBacks.FirstOrDefaultAsync(i => i.FeedbackID == key);
                return doc;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<ICollection<UserFeedBack>?> GetAll()
        {
            try
            {
                var doc = await _context.UserFeedBacks.ToListAsync();
                if (doc.Count > 0)
                    return doc;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<UserFeedBack?> Update(UserFeedBack item)
        {
            try
            {
                var existingDoctor = await _context.UserFeedBacks.FindAsync(item.FeedbackID);
                if (existingDoctor != null)
                {
                    existingDoctor.Comment = existingDoctor.Comment;
                    existingDoctor.Ratings = item.Ratings;
                 


                    await _context.SaveChangesAsync();

                    return existingDoctor;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
