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
        [HttpPost("{questionId}/answer")]
        [Authorize]
        public async Task<ActionResult<Answer>> AnswerQuestion(string questionId, [FromBody] CreateAnswerDto answerDto)
        {
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);  // Get userId from the JWT token

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User is not authenticated." });
            }

            var answer = await _answerService.CreateAnswerAsync(userId, questionId, answerDto);
            return Ok(answer);
        }

       
        [HttpGet("{questionId}")]
        [Authorize]
        public async Task<ActionResult<List<Answer>>> GetAllAnswers(string questionId)
        {
            var answers = await _answerService.GetAnswersByQuestionIdAsync(questionId);
            return Ok(answers);
        }

        // DELETE: /api/answers/{answerId}
        [Authorize]
        [HttpDelete("{answerId}")]
        public async Task<IActionResult> DeleteAnswer(string answerId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized(new { message = "User not authenticated" });

            var success = await _answerService.DeleteAnswerAsync(answerId, userId);
            if (!success) return NotFound(new { message = "Answer not found or unauthorized" });

            return Ok(new { message = "Answer deleted successfully" });
        }

        [Authorize]  
        [HttpPut("{answerId}")]
        public async Task<IActionResult> UpdateAnswer(string answerId, [FromBody] CreateAnswerDto updateDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized(new { message = "User not authenticated" });

            var success = await _answerService.UpdateAnswerAsync(answerId, userId, updateDto.Body);
            if (!success) return NotFound(new { message = "Answer not found or unauthorized" });

            return Ok(new { message = "Answer updated successfully" });
        }
    }
}