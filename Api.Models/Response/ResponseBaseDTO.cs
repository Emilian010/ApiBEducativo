namespace Api.Models.Response
{
    using Newtonsoft.Json;

    /// <summary>
    /// Response base item
    /// </summary>
    public class ResponseBaseDTO
    {
        /// <summary>
        /// Meta
        /// </summary>
        [JsonProperty("meta")]
        public MetaDTO Meta { get; set; }
    }
}
