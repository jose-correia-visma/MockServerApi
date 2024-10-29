using Microsoft.AspNetCore.Mvc;
using MockServerApi.Controllers.Extensions;
using MockServerApi.MockServer;
using MockServerApi.Model;

namespace MockServerApi.Controllers
{
    [ApiController]
    public class MockServerController : ControllerBase
    {
        private HttpClient Client { get; }
    
        [HttpPost("clearExpectations")]
        public async Task<IActionResult> Post(bool clearProductsExpectations)
        {
            if (clearProductsExpectations)
            {
                await MockServerHelper.ClearExpectationsAsync();

                return Ok(clearProductsExpectations);
            }

            return Ok(false);
        }
        
        [HttpPost("setExpectation")]
        public async Task<IActionResult> SetExpectation([FromBody] Expectation expectation, bool clearPreviousExpectations = true)
        {
            if (expectation == null)
            {
                return BadRequest("Invalid expectation data.");
            }
    
            if (string.IsNullOrWhiteSpace(expectation.Method) || string.IsNullOrWhiteSpace(expectation.Url))
            {
                return BadRequest("Method and URL must be specified.");
            }

            if (clearPreviousExpectations)
            {
                await MockServerHelper.ClearExpectationsAsync();
            }

            string expectationJson;

            if (expectation.Method.ToUpper() == "GET")
            {
                expectationJson = expectation.BuildGetExpectationJson();
            }
            else if (expectation.Method.ToUpper() == "POST")
            {
                expectationJson = expectation.BuildPostExpectationJson();
            }
            else
            {
                return BadRequest("Unsupported HTTP method. Only GET and POST are supported.");
            }
    
            try
            {
                await MockServerHelper.SetExpectationAsyncFromJson(expectationJson);
                return Ok(new { message = "Expectation set successfully." });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error setting expectation: " + ex.Message);
                return StatusCode(500, "An error occurred while setting the expectation.");
            }
        }
    }
}