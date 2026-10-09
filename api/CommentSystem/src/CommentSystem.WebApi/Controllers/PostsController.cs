using CommentSystem.Application.Comments.DTOs;
using CommentSystem.Application.Interfaces;
using CommentSystem.Application.Posts.DTOs;
using CommentSystem.Domain.Exceptions;
using CommentSystem.WebApi.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CommentSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly CurrentUserSettings _currentUser;

        public PostsController(
            IPostService postService,
            ICommentService commentService,
            IOptions<CurrentUserSettings> currentUser)
        {
            _postService = postService;
            _commentService = commentService;
            _currentUser = currentUser.Value;
        }

        [HttpGet("{postId:long}")]
        [ProducesResponseType(typeof(ApiResponse<PostDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<PostDto>>> GetById(long postId, CancellationToken cancellationToken)
        {
            var post = await _postService.GetByIdAsync(postId, cancellationToken);

            if (post is null)
            {
                return NotFound(ApiResponse<PostDto>.Fail("Post not found"));
            }

            return Ok(ApiResponse<PostDto>.Ok(post));
        }

        [HttpGet("{postId:long}/comments")]
        [ProducesResponseType(typeof(ApiResponse<CommentPageDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<CommentPageDto>>> GetComments(
            long postId,
            [FromQuery] long? before,
            [FromQuery] int? limit,
            CancellationToken cancellationToken)
        {
            var page = await _commentService.GetByPostAsync(postId, before, limit, cancellationToken);

            if (page is null)
            {
                return NotFound(ApiResponse<CommentPageDto>.Fail("Post not found"));
            }

            return Ok(ApiResponse<CommentPageDto>.Ok(page));
        }

        [HttpPost("{postId:long}/comments")]
        [ProducesResponseType(typeof(ApiResponse<CommentDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<CommentDto>>> CreateComment(
            long postId,
            [FromBody] CreateCommentRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var comment = await _commentService.CreateAsync(postId, _currentUser.UserId, request.Content, cancellationToken);

                if (comment is null)
                {
                    return NotFound(ApiResponse<CommentDto>.Fail("Post not found"));
                }

                return StatusCode(StatusCodes.Status201Created, ApiResponse<CommentDto>.Ok(comment));
            }
            catch (CommentOperationException ex)
            {
                return BadRequest(ApiResponse<CommentDto>.Fail(ex.Message));
            }
        }
    }
}
