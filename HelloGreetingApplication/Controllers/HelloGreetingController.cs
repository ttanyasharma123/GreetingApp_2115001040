using Microsoft.AspNetCore.Mvc;
using ModelLayerr.Model;


namespace HelloGreetingApplication.Controllers
{

    [ApiController]
    [Route("HelloGreetingApplication")]
    public class HelloGreetingController : ControllerBase
    {
        private static Dictionary<string, string> _dataStore = new Dictionary<string, string>();
       [HttpGet]
        public IActionResult Get() {
            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Message = "Hello to Greeting App API EndPoint";
            responseModel.Success = true;
            responseModel.Data = "Hello World!";
            return Ok(responseModel);



        }
        /// <summary>
        ///     Get method to get the greeting message
        /// </summary>
        /// <param name="requestModel"></param>

        /// <returns>response model</returns>



        [HttpPost]
        public IActionResult Post(RequestModel requestModel) {
            _dataStore[requestModel.key]= requestModel.value;
            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Request received successfully";
            responseModel.Data = $"Key: {requestModel.key}, Value:{requestModel.value}";
            return Ok(responseModel);
        }
        
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

