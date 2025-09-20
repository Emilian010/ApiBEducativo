using Api.Models.Utils;

namespace Api.Models.Enums
{
    /// <summary>
    /// Enumeration for status
    /// </summary>
    public enum StatusType
    {
        [StringValue("Success")]
        Success = 1,
        [StringValue("Failed")]
        Failed = 2
    }
}
