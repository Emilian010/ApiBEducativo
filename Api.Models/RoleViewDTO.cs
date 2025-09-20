namespace Api.Models
{
    public class RoleViewDTO
    {
        public int RoleViewId { get; set; }

        public string RolesId { get; set; }

        public int ViewId { get; set; }

        public ViewDTO Views { get; set; }

        public ViewDTO? Parent { get; set; }

        public bool IsView { get; set; }

        public bool IsAdd { get; set; }

        public bool IsUpdate { get; set; }

        public bool IsDelete { get; set; }
    }
}
