using System;

namespace GreetingsService.Models
{
    /// <summary>
    /// Response model matching the MessageService API response
    /// </summary>
    public class MessageResponse
    {
        /// <summary>
        /// The formatted message string
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The timestamp when the message was generated
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
