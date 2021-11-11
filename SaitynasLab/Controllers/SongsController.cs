using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SaitynasLab.Data.Dtos;
using SaitynasLab.Data.Entities;
using SaitynasLab.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynasLab.Controllers
{
    [ApiController]
    [Route("api/concerts/{concertId}/songs")]
    public class SongsController : ControllerBase
    {
        private readonly ISongRepository _songRepository;
        private readonly IConcertRepository _concertRepository;
        private readonly IMapper _mapper;

        public SongsController(ISongRepository songRepository, IConcertRepository concertRepository, IMapper mapper)
        {
            _songRepository = songRepository;
            _concertRepository = concertRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SongDto>> GetAll(int concertId)
        {
            var songs = await _songRepository.GetAsync(concertId);
            return songs.Select(o => _mapper.Map<SongDto>(o));
        }

        [HttpGet("{songId}")]
        public async Task<ActionResult<SongDto>> Get(int songId, int concertId)
        {
            var song = await _songRepository.GetAsync(concertId, songId);
            if (song == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SongDto>(song));
        }

        [HttpPost]
        public async Task<ActionResult<SongDto>> Post(int concertId, CreateSongDto songDto)
        {
            var concert = await _concertRepository.GetAsync(concertId);
            if (concert == null) return NotFound($"Couldn't find a concert with id: {concertId}");

            var song = _mapper.Map<Song>(songDto);
            song.ConcertId = concertId;

            await _songRepository.InsertAsync(song);
            return Created($"api/concerts/{concertId}/songs/{song.Id}", _mapper.Map<SongDto>(song));
        }

        [HttpPut("{songId}")]
        public async Task<ActionResult<SongDto>> Put(int concertId, int songId, UpdateSongDto songDto)
        {
            var concert = await _concertRepository.GetAsync(concertId);
            if (concert == null) return NotFound($"Couldn't find a concert with id: {concertId}");

            var oldSong = await _songRepository.GetAsync(concertId, songId);
            if (oldSong == null) return NotFound($"Couldn't find a song with id: {songId}");

            _mapper.Map(songDto, oldSong);

            await _songRepository.UpdateAsync(oldSong);
            return Ok(_mapper.Map<SongDto>(oldSong));
        }

        [HttpDelete("{songId}")]
        public async Task<ActionResult> Delete(int songId, int concertId)
        {
            var song = await _songRepository.GetAsync(concertId, songId);
            if (song == null) return NotFound();

            await _songRepository.DeleteAsync(song);
            return NoContent();
        }
    }
}
