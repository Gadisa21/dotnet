using CommunityForum.DTOs;
using CommunityForum.Models;
using CommunityForum.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CommunityForum.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        // Injecting IAnswerService via constructor
        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        // POST api/answers/{id}/answer
        [HttpPost("{id}/answer")]
        [Authorize]
        public async Task<ActionResult<Answer>> AnswerQuestion(string id, [FromBody] CreateAnswerDto answerDto)
        {
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);  // Get userId from the JWT token

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User is not authenticated." });
            }

            var answer = await _answerService.CreateAnswerAsync(userId, id, answerDto);
            return Ok(answer);
        }

        // GET api/answers/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Answer>>> GetAllAnswers(string id)
        {
            var answers = await _answerService.GetAnswersByQuestionIdAsync(id);
            return Ok(answers);
        }
    }
}