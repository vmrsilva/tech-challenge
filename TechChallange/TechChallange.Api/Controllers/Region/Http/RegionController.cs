using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechChallange.Api.Controllers.Region.Dto;
using TechChallange.Domain.Region.Entity;
using TechChallange.Domain.Region.Service;

namespace TechChallange.Api.Controllers.Region.Http
{
    [ApiController]
    [Route("[controller]")]
    public class RegionController : Controller
    {
        private readonly IRegionService _regionService;
        private readonly IMapper _mapper;

        public RegionController(IRegionService regionService, IMapper mapper)
        {
            _regionService = regionService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RegionCreateDto region)
        {
            var regionEntity = _mapper.Map<RegionEntity>(region);
            await _regionService.Create(regionEntity).ConfigureAwait(false);

            return Ok();
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionEntity = await _regionService.GetById(id).ConfigureAwait(false);
            var regionDto = _mapper.Map<RegionResponseDto>(regionEntity);
            return Ok(regionDto);
        }

        [HttpGet("get-by-ddd/{ddd}")]
        public async Task<IActionResult> GetByDdd([FromRoute] string ddd)
        {
            var regionEntity = await _regionService.GetByDdd(ddd).ConfigureAwait(false);
            var regionDto = _mapper.Map<RegionResponseDto>(regionEntity);
            return Ok(regionDto);
        }
    }
}
