
namespace Api.Models
{
    public class ViewDTO
    {
        public int ViewId { get; set; }
        public string? Url { get; set; }
        public string Description { get; set; } = string.Empty;

        public bool Allow { get; set; }

        public int? ParentId { get; set; }
        public ViewDTO? Parent { get; set; }

    }
}
