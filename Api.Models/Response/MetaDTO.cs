namespace Api.Models.Response
{
    using Api.Models.Common;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Meta response object
    /// </summary>
    public class MetaDTO
    {
        /// <summary>
        /// status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Time stamp
        /// </summary>
        [JsonProperty("timeStamp")]
        public string TimeStamp { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        [JsonProperty("messages")]
        public List<ErrorDTO> Messages { get; set; }

        /// <summary>
        /// Total records
        /// </summary>
        [JsonProperty("totalRecords")]
        public int TotalRecords { get; set; }
    }
}
