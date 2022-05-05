using System;
using System.Threading.Tasks;

using AspNet.Tutorial.Core.DataTransfers.Users;
using AspNet.Tutorial.Core.Entities;
using AspNet.Tutorial.Core.Exceptions;
using AspNet.Tutorial.Core.Interfaces.IServices;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNet.Tutorial.WebApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out Guid guid))
            {
                throw new RecordNotFoundException("Product not found in database");
            }

            User user = await _userService.Get(guid);
            return Ok(_mapper.Map<UserDto>(user));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] UserModificationDto userModificationDto)
        {
            if (!Guid.TryParse(id, out Guid guid))
            {
                throw new RecordNotFoundException("Product not found in database");
            }

            User user = await _userService.Update(guid, userModificationDto);
            return Ok(_mapper.Map<UserDto>(user));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out Guid guid))
            {
                throw new RecordNotFoundException("Product not found in database");
            }

            User user = await _userService.Delete(guid);
            return Ok(_mapper.Map<UserDto>(user));
        }
    }
}
