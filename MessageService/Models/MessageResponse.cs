using System;

namespace MessageService.Models
{
    /// <summary>
    /// Response model for the message endpoint
    /// </summary>
    public class MessageResponse
    {
        /// <summary>
        /// The formatted message string with timestamp
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The timestamp when the message was generated
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
