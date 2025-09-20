namespace Api.Models.Response
{
    using System.Collections.Generic;
    /// <summary>
    /// Defining response Dto with dynamic type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseListDTO<T> : ResponseBaseDTO
    {
        /// <summary>
        /// Response items
        /// </summary>
        public List<T> data { get; set; }
    }
}
