using Microsoft.EntityFrameworkCore;
using SaitynasLab.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynasLab.Data.Repositories
{
    public interface IPartRepository
    {
        Task DeleteAsync(Part part);
        Task<List<Part>> GetAsync(int songId);
        Task<Part> GetAsync(int songId, int partId);
        Task InsertAsync(Part part);
        Task UpdateAsync(Part part);
    }

    public class PartRepository : IPartRepository
    {
        private readonly DemoRestContext _restContext;

        public PartRepository(DemoRestContext restContext)
        {
            _restContext = restContext;
        }

        public async Task<Part> GetAsync(int songId, int partId)
        {
            return await _restContext.Parts.FirstOrDefaultAsync(o => o.SongId == songId && o.Id == partId);
        }

        // All songs from a concert
        public async Task<List<Part>> GetAsync(int songId)
        {
            return await _restContext.Parts.Where(o => o.SongId == songId).ToListAsync();
        }

        public async Task InsertAsync(Part part)
        {
            _restContext.Parts.Add(part);
            await _restContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Part part)
        {
            _restContext.Parts.Update(part);
            await _restContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Part part)
        {
            _restContext.Parts.Remove(part);
            await _restContext.SaveChangesAsync();
        }
    }
}
