using Api.Models;
using Api.Models.Response;

namespace Api.Business.Contracts
{
    public interface IRoleViewsManager
    {
        Task<ResponseListDTO<RoleViewDTO>> GetRoleViewList(string? idRole);
        Task<ResponseItemDTO<bool>> SetRoleView(List<RoleViewDTO> request);
    }
}
