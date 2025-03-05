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
        private static Dictionary<int, string> _dataStore = new Dictionary<int, string>(); // Changed key to int
        private readonly IGreetingBL _greetingBL;

        public HelloGreetingController(IGreetingBL greetingBL)
        {
            _greetingBL = greetingBL;
        }

        /// <summary>
        /// Get method to return greeting message from Service Layer
        /// </summary>
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
        /// Put method to update an existing greeting by ID (Full Update)
        /// </summary>
        [HttpPut("updateGreeting/{id}")]
        public IActionResult UpdateGreeting(int id, [FromBody] GreetingRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Invalid request data"
                });
            }

            var existingGreeting = _greetingBL.FindGreetingById(id);
            if (existingGreeting == null)
            {
                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Greeting not found"
                });
            }

            _greetingBL.SaveGreeting(id, request.Message); // Update greeting

            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting updated successfully",
                Data = request.Message
            });
        }

        /// <summary>
        /// Patch method to update part of a greeting message by ID (Partial Update)
        /// </summary>
        [HttpPatch("updateGreetingPartial/{id}")]
        public IActionResult UpdateGreetingPartial(int id, [FromBody] GreetingRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Invalid request data"
                });
            }

            var existingGreeting = _greetingBL.FindGreetingById(id);
            if (existingGreeting == null)
            {
                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Greeting not found"
                });
            }

            _greetingBL.SaveGreeting(id, request.Message); // Update greeting

            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting partially updated successfully",
                Data = request.Message
            });
        }

        /// <summary>
        /// Delete method to remove a greeting message by ID
        /// </summary>
        [HttpDelete("deleteGreeting/{id}")]
        public IActionResult DeleteGreeting(int id)
        {
            var existingGreeting = _greetingBL.FindGreetingById(id);
            if (existingGreeting == null)
            {
                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Greeting not found"
                });
            }

            _dataStore.Remove(id); // Remove greeting from dictionary

            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = $"Greeting with ID {id} deleted successfully"
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
