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
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        // POST api/questions (Create Question)
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Question>> CreateQuestion([FromBody] CreateQuestionDto questionDto)
        {
            // Extract user ID from JWT token (you can do this through User.Identity or Claims)
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User is not authenticated." });
            }

            try
            {
                var createdQuestion = await _questionService.CreateQuestionAsync(userId, questionDto);
                return CreatedAtAction(nameof(GetQuestionById), new { id = createdQuestion.Id }, createdQuestion);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

       
       
    }
}