using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public SongsController(ISongRepository songRepository, IConcertRepository concertRepository, IMapper mapper, 
            UserManager<IdentityUser> userManager, IAuthorizationService authorizationService)
        {
            _songRepository = songRepository;
            _concertRepository = concertRepository;
            _mapper = mapper;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SongDto>>> GetAll(int concertId)
        {
            // Check if concert exists
            var concert = await _concertRepository.GetAsync(concertId);
            if (concert != null)
            {
                var songs = await _songRepository.GetAsync(concertId);
                return songs.Select(o => _mapper.Map<SongDto>(o)).ToList();
            }
            else return NotFound();
        }

        [HttpGet("{songId}")]
        public async Task<ActionResult<SongDto>> Get(int songId, int concertId)
        {
            var concert = await _concertRepository.GetAsync(concertId);
            if (concert == null) return NotFound();

            var song = await _songRepository.GetAsync(concertId, songId);
            if (song == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SongDto>(song));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Creator")]
        public async Task<ActionResult<SongDto>> Post(int concertId, CreateSongDto songDto)
        {
            var concert = await _concertRepository.GetAsync(concertId);
            if (concert == null) return NotFound($"Couldn't find a concert with id: {concertId}");

            var song = _mapper.Map<Song>(songDto);
            
            song.Concerts.Add(concert);

            song.UserId = _userManager.GetUserId(User);

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

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, oldSong, "SameUser");
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            _mapper.Map(songDto, oldSong);

            await _songRepository.UpdateAsync(oldSong);
            return Ok(_mapper.Map<SongDto>(oldSong));
        }

        [HttpDelete("{songId}")]
        public async Task<ActionResult> Delete(int songId, int concertId)
        {
            var song = await _songRepository.GetAsync(concertId, songId);
            if (song == null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, song, "SameUser");
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            await _songRepository.DeleteAsync(song);
            return NoContent();
        }
    }
}
