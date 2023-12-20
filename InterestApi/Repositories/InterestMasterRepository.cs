using InterestMasterApi.Data;
using InterestMasterApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterestMasterApi.Repositories
{
    public class InterestMasterRepository : IInterestMasterRepository
    {
        private InterestMasterDBContext _context;
        private ILogger<InterestMasterDBContext> _logger;
        public InterestMasterRepository(InterestMasterDBContext context, ILogger<InterestMasterDBContext> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public async Task<IEnumerable<InterestMaster>> GetAccountInterestsAsync()
        {
            try
            {
                var interests = await _context.InterestMasters.ToListAsync();
                return interests;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it more appropriately
                Console.WriteLine($"An error occurred in GetAccountInterestsAsync: {ex.Message}");
                throw; // Rethrow the exception or handle it as needed
            }
        }

    }
}
