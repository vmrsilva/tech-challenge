using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechChallange.Api.Controllers.Region.Dto;
using TechChallange.Domain.Region.Entity;
using TechChallange.Domain.Region.Exception;
using TechChallange.Domain.Region.Service;

namespace TechChallange.Api.Controllers.Region.Http
{
    [ApiController]
    [Route("[controller]")]
    public class RegionController : Controller
    {
        private readonly IRegionService _regionService;
        private readonly IMapper _mapper;

        public RegionController(IRegionService regionService,
                                IMapper mapper)
        {
            _regionService = regionService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RegionCreateDto regionDto)
        {
            try
            {
                var regionEntity = _mapper.Map<RegionEntity>(regionDto);
                await _regionService.CreateAsync(regionEntity).ConfigureAwait(false);

                return Ok();
            }
            catch (RegionAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro!");
            }

        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                var regionEntity = await _regionService.GetByIdAsync(id).ConfigureAwait(false);
                var regionDto = _mapper.Map<RegionResponseDto>(regionEntity);
                return Ok(regionDto);
            }
            catch (RegionNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro");
            }

        }

        [HttpGet("get-by-ddd/{ddd}")]
        public async Task<IActionResult> GetByDdd([FromRoute] string ddd)
        {
            var regionEntity = await _regionService.GetByDdd(ddd).ConfigureAwait(false);
            var regionDto = _mapper.Map<RegionResponseDto>(regionEntity);
            return Ok(regionDto);
        }

        [HttpGet("get-ddd-with-contacts/{ddd}")]
        public async Task<IActionResult> GetByDddWithContacts([FromRoute] string ddd)
        {
            var regionEntity = await _regionService.GetByDddWithContacts(ddd).ConfigureAwait(false);
            var regionDto = _mapper.Map<RegionWithContactsResponseDto>(regionEntity);
            return Ok(regionDto);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var regions = await _regionService.GetAllAsync().ConfigureAwait(false);

            var response = _mapper.Map<IEnumerable<RegionResponseDto>>(regions);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] RegionUpdateDto regionDto)
        {
            try
            {
                var regionEntity = _mapper.Map<RegionEntity>(regionDto);

                await _regionService.UpdateAsync(regionEntity).ConfigureAwait(false);

                return Ok();
            }
            catch (RegionNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro!");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            try
            {
                await _regionService.DeleteByIdAsync(id).ConfigureAwait(false);

                return Ok();
            }
            catch (RegionNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro!");
            }

        }
    }
}
