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

            await _contactService.CreateAsync(contactEntity).ConfigureAwait(false);

            return Ok();
        }

        [HttpGet("by-ddd/{ddd}")]
        public async Task<IActionResult> GetByDddAsync([FromRoute] string ddd)
        {
            var contacts = await _contactService.GetByDddAsync(ddd).ConfigureAwait(false);

            return Ok(contacts);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var contacts = await _contactService.GetAllAsync().ConfigureAwait(false);

            var response = _mapper.Map<IEnumerable<ContactResponseDto>>(contacts);
            return Ok(response);
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            try
            {
                var contact = _contactService.GetByIdAsync(id).ConfigureAwait(false);

                var response = _mapper.Map<ContactResponseDto>(contact);

                return Ok(response);
            }
            catch (ContactNotFoundException)
            {
                return BadRequest("Contato não encontrado na base de dados.");
            }

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            try
            {
                await _contactService.RemoveByIdAsync(id).ConfigureAwait(false);
            }
            catch (ContactNotFoundException)
            {
                return BadRequest("Contato informado não encontrado na base de dados");
            }

            return Ok();
        }

    }
}
