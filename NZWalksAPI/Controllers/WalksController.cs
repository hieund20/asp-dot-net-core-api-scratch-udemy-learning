using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    //api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        //CREATE walk
        //POST: /api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            if(ModelState.IsValid)
            {
                // Map DTO to Domain Model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                await walkRepository.CreateAsync(walkDomainModel);

                //Map Domain Model to DTO
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //GET Walks
        //GET /api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await walkRepository.GetAllAsync();

            //Map Domain Model to DTO
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        //GET Walk by Id
        //GET: /api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to Dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //UPDATE Walk By Id
        //PUT: /api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            if(ModelState.IsValid)
            {
                //Map Dto to Domain model
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                {
                    return NotFound();
                }

                //Map Domain model to Dto
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            }
            else
            {
                return BadRequest(ModelState);
            } 
        }

        //Delete a Walk By Id
        //DELETE: /api/Walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteWalkDomainModel =  await walkRepository.DeleteAsync(id);

            if (deleteWalkDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model to Dto
            return Ok(mapper.Map<WalkDto>(deleteWalkDomainModel));
        }
    }
}
