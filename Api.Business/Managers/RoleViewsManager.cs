using Api.Business.Contracts;
using Api.Business.Response;
using Api.Data.Data;
using Api.Models;
using Api.Models.Common;
using Api.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace Api.Business.Managers
{
    public class RoleViewsManager : IRoleViewsManager
    {

        private readonly ApplicationDbContext _context;

        public RoleViewsManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseListDTO<RoleViewDTO>> GetRoleViewList(string? idRole)
        {
            try
            {
                var viewList = await GetViews(idRole);


                return ResponseData.ResponseSuccess(viewList);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO { Code = "1000", Message = "Error al consultar los servicios" };
                return ResponseData.ResponseListFailed<RoleViewDTO>(error);
            }

        }

        public async Task<ResponseItemDTO<bool>> SetRoleView(List<RoleViewDTO> request)
        {
            var error = new ErrorDTO { Code = "1100", Message = "Error al guardar permisos del rol" };
            try
            {

                if (request == null || !request.Any())
                {
                    return ResponseData.ResponseSuccess(false);
                }

                var idRol = request.FirstOrDefault().RolesId;

                var deletePrev = await _context.ViewRols.Where(x => x.RolesId == idRol).ToListAsync();
                if (deletePrev != null && deletePrev.Any())
                {
                    deletePrev.ForEach(x => _context.ViewRols.Remove(x));
                }

                var Role = _context.Roles.Where(x => x.Id == idRol).FirstOrDefault();
                request.ForEach(y =>
                {
                    var view = _context.Views.Where(x => x.ViewId == y.Views.ViewId).FirstOrDefault();
                    if (view != null && Role != null)
                    {

                        var configRoleView = new Data.Models.RoleView
                        {
                            Views = view,
                            Roles = Role,
                            IsAdd = y.IsAdd,
                            IsDelete = y.IsDelete,
                            IsUpdate = y.IsUpdate,
                            IsView = y.IsView
                        };
                        _context.ViewRols.AddAsync(configRoleView);
                    }

                });

                var resultado = await _context.SaveChangesAsync();

                return resultado > 0 ? ResponseData.ResponseSuccess(true) : ResponseData.ResponseFailed<bool>(error);

            }
            catch (Exception ex)
            {

                return ResponseData.ResponseFailed<bool>(error);
            }
        }


        private async Task<List<RoleViewDTO>> GetViews(string id)
        {
            var response = new List<RoleViewDTO>();
            var viewList = await _context.Views.ToListAsync();


            var viewParent = viewList.Where(x => x.ParentId != null).Select(x => x.ParentId).Distinct();

            var views = viewList.Where(x => viewParent.All(p => p != x.ViewId)).Select(x => x);
            var ViewRol = await _context.ViewRols.Where(x => x.RolesId == id).Select(x => x).ToListAsync();


            response = (from v in views
                        join vr in ViewRol
                           on v.ViewId equals vr.Views.ViewId into grouping
                        from vlist in grouping.DefaultIfEmpty()

                        select new RoleViewDTO
                        {
                            RolesId = vlist != null ? vlist.RolesId : id,
                            IsAdd = vlist != null ? vlist.IsAdd : false,
                            IsView = vlist != null ? vlist.IsView : false,
                            IsUpdate = vlist != null ? vlist.IsUpdate : false,
                            IsDelete = vlist != null ? vlist.IsDelete : false,
                            Views = new ViewDTO { Description = v.Description, ViewId = v.ViewId },
                            Parent = v.Parent != null ? new ViewDTO { Description = v.Parent.Description, ViewId = v.Parent.ViewId } : new ViewDTO()
                        }).ToList();

            return response;
        }

    }
}
