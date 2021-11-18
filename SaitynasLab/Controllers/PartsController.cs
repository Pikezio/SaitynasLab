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
    [Route("api/concerts/{concertId}/songs/{songId}/parts")]
    public class PartsController : ControllerBase
    {
        private readonly ISongRepository _songRepository;
        private readonly IPartRepository _partRepository;
        private readonly IConcertRepository _concertRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<IdentityUser> _userManager;

        public PartsController(IPartRepository partRepository, ISongRepository songRepository, 
            IConcertRepository concertRepository, IMapper mapper, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
        {
            _songRepository = songRepository;
            _partRepository = partRepository;
            _concertRepository = concertRepository;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Creator,Musician")]
        public async Task<IEnumerable<PartDto>> GetAll(int songId)
        {
            var parts = await _partRepository.GetAsync(songId);
            return parts.Select(o => _mapper.Map<PartDto>(o));
        }


        [HttpGet("{partId}")]
        [Authorize(Roles = "Admin,Creator,Musician")]
        public async Task<ActionResult<PartDto>> Get(int concertId, int songId, int partId)
        {
            var song = await _songRepository.GetAsync(concertId, songId);
            if (song is null) return NotFound();

            var part = await _partRepository.GetAsync(songId, partId);
            if (part == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PartDto>(part));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Creator")]
        public async Task<ActionResult<PartDto>> Post(int concertId, int songId, CreatePartDto partDto)
        {
            var song = await _songRepository.GetAsync(songId);
            if (song == null) return NotFound();

            var part = _mapper.Map<Part>(partDto);
            part.SongId = songId;
            part.UserId = _userManager.GetUserId(User);

            await _partRepository.InsertAsync(part);
            return Created($"api/concerts/{concertId}/songs/{songId}/parts/{part.Id}", _mapper.Map<PartDto>(part));
        }

        [HttpPut("{partId}")]
        [Authorize(Roles = "Admin,Creator")]
        public async Task<ActionResult<PartDto>> Put(int concertId, int partId, int songId, UpdatePartDto partDto)
        {
            var song = await _songRepository.GetAsync(concertId, songId);
            if (song == null) return NotFound();

            var oldPart = await _partRepository.GetAsync(songId, partId);
            if (oldPart == null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, song, "SameUser");
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            _mapper.Map(partDto, oldPart);

            await _partRepository.UpdateAsync(oldPart);
            return Ok(_mapper.Map<PartDto>(oldPart));
        }

        [HttpDelete("{partId}")]
        [Authorize(Roles = "Admin,Creator")]
        public async Task<ActionResult> Delete(int concertId, int songId, int partId)
        {
            var song = await _songRepository.GetAsync(concertId, songId);
            if (song == null) return NotFound();

            var part = await _partRepository.GetAsync(songId, partId);
            if (part == null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, song, "SameUser");
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            await _partRepository.DeleteAsync(part);
            return NoContent();
        }
    }
}
