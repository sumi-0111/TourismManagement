using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FeedBackApi.Models;
using Microsoft.Extensions.Logging;
using FeedBackApi.Interface; 

namespace FeedBackApi.Services
{
    public class FeedbackServices : IRepo<FeedBack>
    {
        private readonly FeedBackContext _context;
        private readonly ILogger<FeedbackServices> _logger;

        public FeedbackServices(FeedBackContext context, ILogger<FeedbackServices> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<FeedBack?> Add(FeedBack item)
        {
            try
            {
                var addedItem = await _context.FeedBacks.AddAsync(item);
                await _context.SaveChangesAsync();
                return addedItem.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding feedback.");
                return null;
            }
        }

        public async Task<ICollection<FeedBack>?> GetAll()
        {
            try
            {
                return await _context.FeedBacks.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving feedbacks.");
                return null;
            }
        }
    }
}
