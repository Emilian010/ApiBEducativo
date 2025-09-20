using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Api.Data.AspNet;

namespace Api.Data.Models
{
    public class View
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ViewId { get; set; }
        public string? Url { get; set; }

        public string Description { get; set; } = string.Empty;

        public bool Allow { get; set; }

        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        public virtual View? Parent { get; set; }

        public List<AspNetRoles>? Roles { get; }
    }
}
