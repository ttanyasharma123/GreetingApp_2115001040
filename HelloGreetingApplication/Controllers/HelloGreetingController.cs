using BusinessLayerr.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayerr.Model;
using System.Collections.Generic;

namespace HelloGreetingApplication.Controllers
{
    [ApiController]
    [Route("HelloGreetingApplication")]
    public class HelloGreetingController : ControllerBase
    {
        private static Dictionary<string, string> _dataStore = new Dictionary<string, string>();
        private readonly IGreetingBL _greetingBL;

        public HelloGreetingController(IGreetingBL greetingBL)
        {
            _greetingBL = greetingBL;
        }

        /// <summary>
        /// Get method to return greeting message from Service Layer
        /// </summary>
        /// <returns>Response Model</returns>
        [HttpGet("greet")]
        public IActionResult GetGreeting([FromQuery] string? firstName, [FromQuery] string? lastName)
        {
            string message = _greetingBL.GetGreetingMessage(firstName, lastName);

            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting message retrieved successfully",
                Data = message
            });
        }

        /// <summary>
        /// Get method to retrieve a greeting message by ID
        /// </summary>
        [HttpGet("getGreetingById/{id}")]
        public IActionResult GetGreetingById(int id)
        {
            var message = _greetingBL.FindGreetingById(id);
            if (message != null)
            {
                return Ok(new ResponseModel<string>
                {
                    Success = true,
                    Message = "Greeting message retrieved successfully",
                    Data = message
                });
            }
            return NotFound(new ResponseModel<string>
            {
                Success = false,
                Message = "Greeting not found"
            });
        }

        /// <summary>
        /// Post method to save a greeting message with an ID
        /// </summary>
        [HttpPost("saveGreeting")]
        public IActionResult SaveGreeting([FromBody] GreetingRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Greeting message cannot be empty"
                });
            }

            _greetingBL.SaveGreeting(request.Id, request.Message);

            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting saved successfully!",
                Data = request.Message
            });
        }

        /// <summary>
        /// Get all saved greeting messages
        /// </summary>
        [HttpGet("getAllGreetings")]
        public IActionResult GetAllGreetings()
        {
            var greetings = _greetingBL.GetAllGreetings();

            return Ok(new ResponseModel<List<string>>
            {
                Success = true,
                Message = "All greetings retrieved successfully",
                Data = greetings
            });
        }
    }

    /// <summary>
    /// DTO for saving a greeting
    /// </summary>
    public class GreetingRequest
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }
}
