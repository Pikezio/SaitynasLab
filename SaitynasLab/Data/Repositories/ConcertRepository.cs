using Microsoft.EntityFrameworkCore;
using SaitynasLab.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynasLab.Data.Repositories
{
    public interface IConcertRepository
    {
        Task<Concert> InsertAsync(Concert concert);
        Task DeleteAsync(Concert concert);
        Task<Concert> GetAsync(int id);
        Task<IEnumerable<Concert>> GetAsync();
        Task UpdateAsync(Concert concert);
    }

    public class ConcertRepository : IConcertRepository
    {
        private readonly DemoRestContext restContext;

        public ConcertRepository(DemoRestContext restContext)
        {
            this.restContext = restContext;
        }

        public async Task<IEnumerable<Concert>> GetAsync()
        {
            return await restContext.Concerts.ToListAsync();
        }

        public async Task<Concert> GetAsync(int id)
        {
            return await restContext.Concerts.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Concert> InsertAsync(Concert concert)
        {
            restContext.Concerts.Add(concert);
            await restContext.SaveChangesAsync();
            return concert;
        }

        public async Task UpdateAsync(Concert concert)
        {
            restContext.Concerts.Update(concert);
            await restContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Concert concert)
        {
            restContext.Concerts.Remove(concert);
            await restContext.SaveChangesAsync();
        }
    }
}
