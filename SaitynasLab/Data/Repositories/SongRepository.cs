using Microsoft.EntityFrameworkCore;
using SaitynasLab.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynasLab.Data.Repositories
{
    public interface ISongRepository
    {
        Task DeleteAsync(Song song);
        Task<List<Song>> GetAsync(int concertId);
        Task<Song> GetAsync(int concertId, int songId);
        Task InsertAsync(Song song);
        Task UpdateAsync(Song song);
    }

    public class SongRepository : ISongRepository
    {
        private readonly RestContext _restContext;

        public SongRepository(RestContext restContext)
        {
            _restContext = restContext;
        }

        // Specific song from a specific concert
        public async Task<Song> GetAsync(int concertId, int songId)
        {
            var songs = await _restContext.Concerts.Where(c => c.Id == concertId).SelectMany(c => c.Songs).ToListAsync();
            var song = songs.FirstOrDefault(s => s.Id == songId);
            return song;
        }

        // All songs from a concert
        public async Task<List<Song>> GetAsync(int concertId)
        {
            var songs = _restContext.Concerts.Where(c => c.Id == concertId).SelectMany(c => c.Songs).ToListAsync();
            return await songs;
        }

        public async Task InsertAsync(Song song)
        {
            _restContext.Songs.Add(song);
            await _restContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Song song)
        {
            _restContext.Songs.Update(song);
            await _restContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Song song)
        {
            _restContext.Songs.Remove(song);
            await _restContext.SaveChangesAsync();
        }
    }
}
