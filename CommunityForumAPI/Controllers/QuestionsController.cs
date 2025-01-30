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
                return Created($"/api/questions/{createdQuestion.Id}", createdQuestion);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/questions (Get All Questions with Pagination)
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Question>>> GetAllQuestions([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            // Validate pagination parameters
            if (page <= 0)
            {
                return BadRequest(new { message = "Page number must be greater than 0." });
            }

            if (pageSize <= 0 || pageSize > 50)
            {
                return BadRequest(new { message = "Page size must be between 1 and 50." });
            }
            var questions = await _questionService.GetAllQuestionsAsync(page, pageSize);
            return Ok(questions);
        }

        // GET api/questions/{id} (Get a specific question by ID)
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Question>> GetQuestionById(string id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
            {
                return NotFound(new { message = "Question not found" });
            }

            return Ok(question);
        }
        // GET api/questions/search
        [HttpGet("search")]
        [Authorize]
        public async Task<ActionResult<List<Question>>> SearchQuestions([FromQuery] string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return BadRequest(new { message = "Keyword is required." });
            }

            var questions = await _questionService.SearchQuestionsAsync(keyword);
            return Ok(questions);
        }
    


       
       
    }
}