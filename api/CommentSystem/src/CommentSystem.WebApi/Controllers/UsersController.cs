using CommentSystem.Application.Interfaces;
using CommentSystem.Application.Posts.DTOs;
using CommentSystem.WebApi.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CommentSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly CurrentUserSettings _currentUser;

        public UsersController(IUserService userService, IOptions<CurrentUserSettings> currentUser)
        {
            _userService = userService;
            _currentUser = currentUser.Value;
        }

        [HttpGet("me")]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetCurrent(CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(_currentUser.UserId, cancellationToken);

            if (user is null)
            {
                return NotFound(ApiResponse<UserDto>.Fail("User not found"));
            }

            return Ok(ApiResponse<UserDto>.Ok(user));
        }
    }
}
