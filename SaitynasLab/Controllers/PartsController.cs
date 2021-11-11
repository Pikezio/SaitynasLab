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
    [Route("api/concerts/{concertId}/songs/{songId}/parts")]
    public class PartsController : ControllerBase
    {
        private readonly ISongRepository _songRepository;
        private readonly IPartRepository _partRepository;
        private readonly IConcertRepository _concertRepository;
        private readonly IMapper _mapper;

        public PartsController(IPartRepository partRepository, ISongRepository songRepository, IConcertRepository concertRepository, IMapper mapper)
        {
            _songRepository = songRepository;
            _partRepository = partRepository;
            _concertRepository = concertRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PartDto>> GetAll(int songId)
        {
            var parts = await _partRepository.GetAsync(songId);
            return parts.Select(o => _mapper.Map<PartDto>(o));
        }


        [HttpGet("{partId}")]
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
        public async Task<ActionResult<PartDto>> Post(int concertId, int songId, CreatePartDto partDto)
        {
            var song = await _songRepository.GetAsync(songId);
            if (song == null) return NotFound();

            var part = _mapper.Map<Part>(partDto);
            part.SongId = songId;

            await _partRepository.InsertAsync(part);
            return Created($"api/concerts/{concertId}/songs/{songId}/parts/{part.Id}", _mapper.Map<PartDto>(part));
        }

        [HttpPut("{partId}")]
        public async Task<ActionResult<PartDto>> Put(int partId, int songId, UpdatePartDto partDto)
        {
            var song = await _songRepository.GetAsync(songId);
            if (song == null) return NotFound();

            var oldPart = await _partRepository.GetAsync(songId, partId);
            if (oldPart == null) return NotFound();

            _mapper.Map(partDto, oldPart);

            await _partRepository.UpdateAsync(oldPart);
            return Ok(_mapper.Map<PartDto>(oldPart));
        }

        [HttpDelete("{partId}")]
        public async Task<ActionResult> Delete(int songId, int partId)
        {
            var part = await _partRepository.GetAsync(songId, partId);
            if (part == null) return NotFound();

            await _partRepository.DeleteAsync(part);
            return NoContent();
        }
    }
}
