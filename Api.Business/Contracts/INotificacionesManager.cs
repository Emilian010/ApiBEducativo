using Api.Models.Response;
using Api.Models;

namespace Api.Business.Contracts
{
    public interface INotificacionesManager
    {
        Task<ResponseListDTO<NotificacionDTO>> GetList();
    }
}
