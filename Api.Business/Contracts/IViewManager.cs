using Api.Data.Models;
using Api.Models.Response;

namespace Api.Business.Contracts
{
    public interface IViewManager
    {
        Task<ResponseItemDTO<View>> CreateView(View request);

        Task<ResponseListDTO<View>> GetViewList();

        Task<ResponseItemDTO<View>> GetViewItem(int? id);

        Task<ResponseItemDTO<View>> UpdateView(View request);

        Task<ResponseItemDTO<string>> DeleteView(int? id);
    }
}
