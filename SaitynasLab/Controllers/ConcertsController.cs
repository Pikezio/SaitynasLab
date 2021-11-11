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
    [Route("api/concerts")]
    public class ConcertsController : ControllerBase
    {
        private readonly IConcertRepository concertRepository;
        private readonly IMapper mapper;

        public ConcertsController(IConcertRepository concertRepository, IMapper mapper)
        {
            this.concertRepository = concertRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ConcertDto>> GetAll()
        {
            return (await concertRepository.GetAsync()).Select(o => mapper.Map<ConcertDto>(o));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConcertDto>> Get(int id)
        {
            var concert = await concertRepository.GetAsync(id);
            if (concert is null) return NotFound();
            return Ok(mapper.Map<ConcertDto>(concert));
        }

        [HttpPost]
        public async Task<ActionResult<ConcertDto>> Post(CreateConcertDto concertDto)
        {
            var concert = mapper.Map<Concert>(concertDto);
            await concertRepository.InsertAsync(concert);

            return Created($"/api/concerts/{concert.Id}", mapper.Map<ConcertDto>(concert));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ConcertDto>> Put(int id, UpdateConcertDto updatedConcert)
        {
            var concert =  await concertRepository.GetAsync(id);
            if (concert is null) return NotFound();

            mapper.Map(updatedConcert, concert);
            await concertRepository.UpdateAsync(concert);

            return Ok(mapper.Map<ConcertDto>(concert));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ConcertDto>> Delete(int id)
        {
            var concert = await concertRepository.GetAsync(id);
            if (concert is null) return NotFound();

            await concertRepository.DeleteAsync(concert);
            return NoContent();
        }
    }
}
