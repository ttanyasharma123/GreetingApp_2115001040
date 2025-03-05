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

            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting message retrieved successfully",
                Data = message
            };

            return Ok(responseModel);
        }

        /// <summary>
        /// Post method to save a greeting message
        /// </summary>
        [HttpPost("saveGreeting")]
        public IActionResult SaveGreeting([FromBody] string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return BadRequest(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Greeting message cannot be empty"
                });
            }

            _greetingBL.SaveGreeting(message);

            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting saved successfully!",
                Data = message
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

        /// <summary>
        /// Post method to store key-value pair
        /// </summary>
        [HttpPost]
        public IActionResult Post(RequestModel requestModel)
        {
            _dataStore[requestModel.key] = requestModel.value;
            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Request received successfully",
                Data = $"Key: {requestModel.key}, Value: {requestModel.value}"
            });
        }

        /// <summary>
        /// Put method to update data
        /// </summary>
        [HttpPut]
        public IActionResult Put([FromBody] RequestModel requestModel)
        {
            if (requestModel == null || string.IsNullOrEmpty(requestModel.key))
            {
                return BadRequest(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Invalid request data"
                });
            }

            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Data updated successfully",
                Data = $"Key: {requestModel.key}, Value: {requestModel.value}"
            });
        }

        /// <summary>
        /// Patch method to update a specific key
        /// </summary>
        [HttpPatch]
        public IActionResult Patch([FromBody] RequestModel requestModel)
        {
            if (requestModel == null || string.IsNullOrEmpty(requestModel.key))
            {
                return BadRequest(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Invalid request data"
                });
            }

            if (!_dataStore.ContainsKey(requestModel.key))
            {
                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Key not found"
                });
            }

            _dataStore[requestModel.key] = requestModel.value;

            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Data updated successfully",
                Data = $"Key: {requestModel.key}, Value: {requestModel.value}"
            });
        }

        /// <summary>
        /// Delete method to remove a key-value pair
        /// </summary>
        [HttpDelete]
        public IActionResult Delete(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return BadRequest(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Invalid request data"
                });
            }

            if (!_dataStore.ContainsKey(key))
            {
                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Key not found"
                });
            }

            _dataStore.Remove(key);

            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Data deleted successfully",
                Data = $"Deleted Key: {key}"
            });
        }
    }
}
