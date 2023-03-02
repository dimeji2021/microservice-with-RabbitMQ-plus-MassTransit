using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using play.identity.service.Dtos;
using play.identity.service.Entities;
using static IdentityServer4.IdentityServerConstants;

namespace play.identity.service.Contoller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = LocalApi.PolicyName, Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userManager.Users
                .ToList()
                .Select(user => user.AsDto());
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
            {
                return NotFound();
            }
            return user.AsDto();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> PutAsync(Guid id, UpdateUserDtos userDtos)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
            {
                return NotFound();
            }
            user.Email = userDtos.Email;
            user.UserName = userDtos.Email;
            user.Gil = userDtos.Gil;
            await _userManager.UpdateAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
            {
                return NotFound();
            }
            await _userManager.DeleteAsync(user);
            return NoContent();
        }
    }
}