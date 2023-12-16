using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalkDBContext dBContext;

        public SQLWalkRepository(NZWalkDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<Walk> CreateAsync (Walk walk)
        {
            await dBContext.Walks.AddAsync(walk);
            await dBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk =  await dBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            
            if (existingWalk != null)
            {
                return null;
            }

            dBContext.Walks.Remove(existingWalk);
            await dBContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await dBContext.Walks.Include("Difficulty") //Navigation Properties
                                        .Include("Region") //Navigation Properties
                                        .ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dBContext.Walks.Include("Difficulty")
                                        .Include("Region")
                                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null) {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await dBContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
