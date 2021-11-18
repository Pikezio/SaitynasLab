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
        private readonly RestContext _restContext;

        public ConcertRepository(RestContext restContext)
        {
            _restContext = restContext;
        }

        public async Task<IEnumerable<Concert>> GetAsync()
        {
            return await _restContext.Concerts.ToListAsync();
        }

        public async Task<Concert> GetAsync(int id)
        {
            return await _restContext.Concerts.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Concert> InsertAsync(Concert concert)
        {
            _restContext.Concerts.Add(concert);
            var x = await _restContext.SaveChangesAsync();
            return concert;
        }

        public async Task UpdateAsync(Concert concert)
        {
            _restContext.Concerts.Update(concert);
            await _restContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Concert concert)
        {
            _restContext.Concerts.Remove(concert);
            await _restContext.SaveChangesAsync();
        }
    }
}
