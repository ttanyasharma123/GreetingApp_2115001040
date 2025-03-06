using BusinessLayerr.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayerr.Model;
using Middleware_Layer.GlobalExceptionHandler;
using System;
using System.Collections.Generic;

namespace HelloGreetingApplication.Controllers
{
    [ApiController]
    [Route("HelloGreetingApplication")]
    public class HelloGreetingController : ControllerBase
    {
        private readonly IGreetingBL _greetingBL;

        public HelloGreetingController(IGreetingBL greetingBL)
        {
            _greetingBL = greetingBL;
        }

        [HttpGet("greet")]
        public IActionResult GetGreeting([FromQuery] string? firstName, [FromQuery] string? lastName)
        {
            try
            {
                string message = _greetingBL.GetGreetingMessage(firstName, lastName);
                return Ok(new ResponseModel<string>
                {
                    Success = true,
                    Message = "Greeting message retrieved successfully",
                    Data = message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string>
                {
                    Success = false,
                    Message = $"Internal Server Error: {ex.Message}"
                });
            }
        }

        [HttpGet("getGreetingById/{id}")]
        public IActionResult GetGreetingById(int id)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string>
                {
                    Success = false,
                    Message = $"Internal Server Error: {ex.Message}"
                });
            }
        }

        [HttpPost("saveGreeting")]
        public IActionResult SaveGreeting([FromBody] GreetingRequest request)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string>
                {
                    Success = false,
                    Message = $"Internal Server Error: {ex.Message}"
                });
            }
        }

        [HttpPut("updateGreeting/{id}")]
        public IActionResult UpdateGreeting(int id, [FromBody] GreetingRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Message))
                {
                    return BadRequest(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "Invalid request data"
                    });
                }

                _greetingBL.UpdateGreeting(id, request.Message);

                return Ok(new ResponseModel<string>
                {
                    Success = true,
                    Message = "Greeting updated successfully",
                    Data = request.Message
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Greeting not found"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string>
                {
                    Success = false,
                    Message = $"Internal Server Error: {ex.Message}"
                });
            }
        }

        [HttpPatch("updateGreetingPartial/{id}")]
        public IActionResult UpdateGreetingPartial(int id, [FromBody] GreetingRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Message))
                {
                    return BadRequest(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "Invalid request data"
                    });
                }

                _greetingBL.UpdateGreeting(id, request.Message);

                return Ok(new ResponseModel<string>
                {
                    Success = true,
                    Message = "Greeting partially updated successfully",
                    Data = request.Message
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Greeting not found"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string>
                {
                    Success = false,
                    Message = $"Internal Server Error: {ex.Message}"
                });
            }
        }

        [HttpDelete("deleteGreeting/{id}")]
        public IActionResult DeleteGreeting(int id)
        {
            try
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

                _greetingBL.DeleteGreeting(id);

                return Ok(new ResponseModel<string>
                {
                    Success = true,
                    Message = $"Greeting with ID {id} deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string>
                {
                    Success = false,
                    Message = $"Internal Server Error: {ex.Message}"
                });
            }
        }

        [HttpGet("getAllGreetings")]
        public IActionResult GetAllGreetings()
        {
            try
            {
                var greetings = _greetingBL.GetAllGreetings();

                return Ok(new ResponseModel<List<string>>
                {
                    Success = true,
                    Message = "All greetings retrieved successfully",
                    Data = greetings
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<List<string>>
                {
                    Success = false,
                    Message = $"Internal Server Error: {ex.Message}"
                });
            }
        }

         
        [HttpGet("throwSerializable")]
        public IActionResult ThrowSerializableException()
        {
            throw new CustomSerializableException("This is a serializable exception!");
        }

        [HttpGet("throwNonSerializable")]
        public IActionResult ThrowNonSerializableException()
        {
            throw new CustomNonSerializableException("This is a non-serializable exception!");
        }

        [HttpGet("throwGeneric")]
        public IActionResult ThrowGenericException()
        {
            throw new Exception("This is a generic exception!");
        }
    }

    public class GreetingRequest
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }
}
