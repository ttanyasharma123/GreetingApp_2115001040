using BusinessLayerr.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayerr.Model;

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
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting message retrieved successfully",
                Data = _greetingBL.GetGreetingMessage(firstName, lastName)
            };

            return Ok(responseModel);
        }

        /// <summary>
        /// Post method to store key-value pair
        /// </summary>
        [HttpPost]
        public IActionResult Post(RequestModel requestModel)
        {
            _dataStore[requestModel.key] = requestModel.value;
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Request received successfully",
                Data = $"Key: {requestModel.key}, Value: {requestModel.value}"
            };
            return Ok(responseModel);
        }

        /// <summary>
        /// Put method to update data
        /// </summary>
        [HttpPut]
        public IActionResult Put([FromBody] RequestModel requestModel)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();

            if (requestModel == null || string.IsNullOrEmpty(requestModel.key))
            {
                responseModel.Success = false;
                responseModel.Message = "Invalid request data";
                return BadRequest(responseModel);
            }

            responseModel.Success = true;
            responseModel.Message = "Data updated successfully";
            responseModel.Data = $"Key: {requestModel.key}, Value: {requestModel.value}";

            return Ok(responseModel);
        }

        /// <summary>
        /// Patch method to update a specific key
        /// </summary>
        [HttpPatch]
        public IActionResult Patch([FromBody] RequestModel requestModel)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();

            if (requestModel == null || string.IsNullOrEmpty(requestModel.key))
            {
                responseModel.Success = false;
                responseModel.Message = "Invalid request data";
                return BadRequest(responseModel);
            }

            if (!_dataStore.ContainsKey(requestModel.key))
            {
                responseModel.Success = false;
                responseModel.Message = "Key not found";
                return NotFound(responseModel);
            }

            // Update the value for the given key
            _dataStore[requestModel.key] = requestModel.value;

            responseModel.Success = true;
            responseModel.Message = "Data updated successfully";
            responseModel.Data = $"Key: {requestModel.key}, Value: {requestModel.value}";

            return Ok(responseModel);
        }

        /// <summary>
        /// Delete method to remove a key-value pair
        /// </summary>
        [HttpDelete]
        public IActionResult Delete(string key)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();

            if (string.IsNullOrEmpty(key))
            {
                responseModel.Success = false;
                responseModel.Message = "Invalid request data";
                return BadRequest(responseModel);
            }

            if (!_dataStore.ContainsKey(key))
            {
                responseModel.Success = false;
                responseModel.Message = "Key not found";
                return NotFound(responseModel);
            }

            // Remove the key-value pair
            _dataStore.Remove(key);

            responseModel.Success = true;
            responseModel.Message = "Data deleted successfully";
            responseModel.Data = $"Deleted Key: {key}";

            return Ok(responseModel);
        }
    }
}
