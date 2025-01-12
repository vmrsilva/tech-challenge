﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechChallange.Api.Controllers.Contact.Dto;
using TechChallange.Api.Response;
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

        public ContactController(IContactService contactService,
                                IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ContactCreateDto contactDto)
        {
            var contactEntity = _mapper.Map<ContactEntity>(contactDto);

            await _contactService.CreateAsync(contactEntity).ConfigureAwait(false);

            return StatusCode(204, new BaseResponse
            {
                Success = true,
                Error = string.Empty
            });
        }

        [HttpGet("by-ddd/{ddd}")]
        public async Task<IActionResult> GetByDddAsync([FromRoute] string ddd)
        {
            var contacts = await _contactService.GetByDddAsync(ddd).ConfigureAwait(false);

            var response = _mapper.Map<IEnumerable<ContactResponseDto>>(contacts);

            return StatusCode(200, new BaseResponseDto<IEnumerable<ContactResponseDto>>
            {
                Success = true,
                Error = string.Empty,
                Data = response
            });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPagedAsync([FromQuery] int pageSize, [FromQuery] int page)
        {
            var contacts = await _contactService.GetAllPagedAsync(pageSize, page).ConfigureAwait(false);

            var totalItem = await _contactService.GetCountAsync().ConfigureAwait(false);

            var response = _mapper.Map<IEnumerable<ContactResponseDto>>(contacts);

            return StatusCode(200, new BaseResponsePagedDto<IEnumerable<ContactResponseDto>>
            {
                Success = true,
                Error = string.Empty,
                Data = response,
                CurrentPage = page,
                TotalItems = totalItem
            });
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            try
            {
                var contact = await _contactService.GetByIdAsync(id).ConfigureAwait(false);

                var response = _mapper.Map<ContactResponseDto>(contact);

                return StatusCode(200, new BaseResponseDto<ContactResponseDto>
                {
                    Success = true,
                    Error = string.Empty,
                    Data = response
                });
            }
            catch (ContactNotFoundException ex)
            {
                return StatusCode(400, new BaseResponse
                {
                    Error = ex.Message,
                    Success = false
                });
            }
            catch (Exception)
            {
                return StatusCode(400, new BaseResponse
                {
                    Error = "Ocorreu um erro!",
                    Success = false
                });
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ContactUpdateDto contactDto)
        {
            try
            {
                var contactEntity = _mapper.Map<ContactEntity>(contactDto);

                await _contactService.UpdateAsync(contactEntity).ConfigureAwait(false);

                return StatusCode(204, new BaseResponse
                {
                    Success = true,
                    Error = string.Empty
                });
            }
            catch (ContactNotFoundException ex)
            {
                return StatusCode(400, new BaseResponse
                {
                    Error = ex.Message,
                    Success = false
                });
            }
            catch (Exception)
            {
                return StatusCode(400, new BaseResponse
                {
                    Error = "Ocorreu um erro!",
                    Success = false
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            try
            {
                await _contactService.RemoveByIdAsync(id).ConfigureAwait(false);

                return StatusCode(204, new BaseResponse
                {
                    Success = true,
                    Error = string.Empty
                });
            }
            catch (ContactNotFoundException ex)
            {
                return StatusCode(400, new BaseResponse
                {
                    Error = ex.Message,
                    Success = false
                });
            }
            catch (Exception)
            {
                return StatusCode(400, new BaseResponse
                {
                    Error = "Ocorreu um erro!",
                    Success = false
                });
            }
        }

    }
}
