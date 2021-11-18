using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SaitynasLab.Auth;
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
        private readonly IConcertRepository _concertRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<IdentityUser> _userManager;

        public ConcertsController(IConcertRepository concertRepository, IMapper mapper, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
        {
            _concertRepository = concertRepository;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IEnumerable<ConcertDto>> GetAll()
        {
            var list = (await _concertRepository.GetAsync()).Select(o => _mapper.Map<ConcertDto>(o));
            return list;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConcertDto>> Get(int id)
        {
            var concert = await _concertRepository.GetAsync(id);
            if (concert is null) return NotFound();
            return Ok(_mapper.Map<ConcertDto>(concert));
        }

        [HttpPost]
        //[Authorize(Roles = UserRoles.Creator)]
        public async Task<ActionResult<ConcertDto>> Post(CreateConcertDto concertDto)
        {
            var concert = _mapper.Map<Concert>(concertDto);

            concert.CreationDate = DateTime.Now;
            concert.UserId = _userManager.GetUserId(User);

            await _concertRepository.InsertAsync(concert);
            return Created($"/api/concerts/{concert.Id}", _mapper.Map<ConcertDto>(concert));
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = UserRoles.Creator)]
        public async Task<ActionResult<ConcertDto>> Put(int id, UpdateConcertDto updatedConcert)
        {
            var concert =  await _concertRepository.GetAsync(id);
            if (concert is null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, concert, "SameUser");
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            _mapper.Map(updatedConcert, concert);
            await _concertRepository.UpdateAsync(concert);

            return Ok(_mapper.Map<ConcertDto>(concert));
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = UserRoles.Creator)]
        public async Task<ActionResult<ConcertDto>> Delete(int id)
        {
            var concert = await _concertRepository.GetAsync(id);
            if (concert is null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, concert, "SameUser");
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            await _concertRepository.DeleteAsync(concert);
            return NoContent();
        }
    }
}
