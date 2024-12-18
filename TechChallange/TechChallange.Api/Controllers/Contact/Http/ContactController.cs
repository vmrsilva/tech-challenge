using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechChallange.Api.Controllers.Contact.Dto;
using TechChallange.Domain.Contact.Entity;
using TechChallange.Domain.Contact.Exception;
using TechChallange.Domain.Contact.Service;

namespace TechChallange.Api.Controllers.Contact.Http
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ContactCreateDto contactDto)
        {
            var contactEntity = _mapper.Map<ContactEntity>(contactDto);

            await _contactService.Create(contactEntity).ConfigureAwait(false);

            return Ok();
        }

        [HttpGet("by-ddd/{ddd}")]
        public async Task<IActionResult> GetByDddAsync([FromRoute] string ddd)
        {
            var contacts = await _contactService.GetByDddAsync(ddd).ConfigureAwait(false);

            return Ok(contacts);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ContactUpdateDto contactDto)
        {
            try
            {
                var contactEntity = _mapper.Map<ContactEntity>(contactDto);

                await _contactService.UpdateAsync(contactEntity).ConfigureAwait(false);

                return Ok();
            }
            catch (ContactNotFoundException)
            {
                return BadRequest("Contato informado não existe na base de dados.");
            }
        }
    }
}
