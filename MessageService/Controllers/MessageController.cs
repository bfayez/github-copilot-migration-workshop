using System;
using Microsoft.AspNetCore.Mvc;
using MessageService.Models;

namespace MessageService.Controllers
{
    /// <summary>
    /// Controller for message-related endpoints
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        /// <summary>
        /// GET endpoint that returns a "Hello World" message with the current date and time prepended.
        /// </summary>
        /// <returns>A MessageResponse object with timestamp</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/message
        ///     
        /// </remarks>
        /// <response code="200">Returns the greeting message with timestamp</response>
        [HttpGet]
        public ActionResult<MessageResponse> GetMessage()
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var message = $"{timestamp} - Hello World";

            var response = new MessageResponse
            {
                Message = message,
                Timestamp = DateTime.Now
            };

            return Ok(response);
        }
    }
}
