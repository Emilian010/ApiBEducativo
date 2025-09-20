namespace Api.Models.Common
{
    using Newtonsoft.Json;
    /// <summary>
    /// Error item
    /// </summary>
    public class ErrorDTO
    {
        /// <summary>
        /// Error code
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Generic error message
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Description error
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
