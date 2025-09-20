namespace Api.Models.Response
{
    /// <summary>
    /// Defining response Dto with dynamic type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseItemDTO<T> : ResponseBaseDTO
    {
        /// <summary>
        /// Response item
        /// </summary>
        public T data { get; set; }
    }
}
