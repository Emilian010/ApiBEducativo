using Api.Business.Contracts;
using Api.Business.Response;
using Api.Data.Data;
using Api.Models.Common;
using Api.Models.Response;
using Api.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Business.Managers
{
    public class UserManager : IUserManager
    {
        private readonly ApplicationDbContext _context;

        public UserManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseListDTO<UsuarioData>> GetUserList()
        {
            try
            {
                var items = (from u in _context.Users
                             join ur in _context.UserRoles
                            on u.Id equals ur.UserId into grouping
                             from vlist in grouping.DefaultIfEmpty()
                             join r in _context.Roles on vlist.RoleId equals r.Id into grouping2
                             from rlist in grouping2.DefaultIfEmpty()
                             select new UsuarioData
                             {
                                 Id = u.Id,
                                 UserName = u.UserName,
                                 PhoneNumber = u.PhoneNumber,
                                 LockoutEnabled = u.LockoutEnabled,
                                 AccessFailedCount = u.AccessFailedCount ?? 0,
                                 Rol = new Rol
                                 {
                                     Id = rlist != null ? rlist.Id : "",
                                     Name = rlist != null ? rlist.Name : ""
                                 }
                             }).ToList();

                return ResponseData.ResponseSuccess(items);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO { Code = "1000", Message = "Error al consultar los User" };
                return ResponseData.ResponseListFailed<UsuarioData>(error);
            }

        }

        public async Task<ResponseItemDTO<UsuarioData>> GetUserItem(string? id)
        {
            try
            {
                var item = await (from u in _context.Users
                                  join ur in _context.UserRoles
                                 on u.Id equals ur.UserId into grouping
                                  from vlist in grouping.DefaultIfEmpty()
                                  join r in _context.Roles on vlist.RoleId equals r.Id into grouping2
                                  from rlist in grouping2.DefaultIfEmpty()
                                  where u.Id == id
                                  select new UsuarioData
                                  {
                                      Id = u.Id,
                                      UserName = u.UserName,
                                      PhoneNumber = u.PhoneNumber,
                                      LockoutEnabled = u.LockoutEnabled,
                                      AccessFailedCount = u.AccessFailedCount ?? 0,
                                      Rol = new Rol
                                      {
                                          Id = rlist != null ? rlist.Id : "",
                                          Name = rlist != null ? rlist.Name : ""
                                      },
                                      RolId = rlist != null ? rlist.Id : "0"
                                  }).FirstOrDefaultAsync();

                return ResponseData.ResponseSuccess(item);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO { Code = "1000", Message = "Error al consultar los User" };
                return ResponseData.ResponseFailed<UsuarioData>(error);
            }

        }

        public async Task<ResponseItemDTO<UsuarioData>> UpdateUser(UsuarioData request)
        {
            var error = new ErrorDTO { Code = "1002", Message = "Error al intentar actualizar el usuario" };
            int resultado = 0;

            try
            {
                if (request == null)
                {
                    return ResponseData.ResponseFailed<UsuarioData>(error);
                }

                var userDB = await _context.Users.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                if (userDB != null)
                {
                    var userRolDB = await _context.UserRoles.Where(x => x.UserId == request.Id).FirstOrDefaultAsync();

                    if (userRolDB != null)
                    {
                        _context.UserRoles.Remove(userRolDB);
                    }

                    _context.UserRoles.Add(new IdentityUserRole<string> { UserId = request.Id, RoleId = request.RolId });

                    userDB.PhoneNumber = request.PhoneNumber;
                    userDB.LockoutEnabled = request.LockoutEnabled;

                    _context.Users.Update(userDB);

                    resultado = await _context.SaveChangesAsync();
                }

                return resultado > 0 ? ResponseData.ResponseSuccess(request) : ResponseData.ResponseFailed<UsuarioData>(error);
            }
            catch (Exception ex)
            {

                return ResponseData.ResponseFailed<UsuarioData>(error);
            }

        }

        public async Task<ResponseItemDTO<string>> DeleteUser(string? id)
        {
            var error = new ErrorDTO { Code = "1003", Message = "Error al intentar eliminar los User" };

            try
            {
                var aspNetUser = await _context.Users.FindAsync(id);
                if (aspNetUser != null)
                {
                    _context.Users.Remove(aspNetUser);

                }
                var mensaje = string.Format("{0} Eliminado correctamente", aspNetUser.UserName);
                var resultado = await _context.SaveChangesAsync();
                return resultado > 0 ? ResponseData.ResponseSuccess(mensaje) : ResponseData.ResponseFailed<string>(error);
            }
            catch (Exception ex)
            {
                return ResponseData.ResponseFailed<string>(error);
            }

        }

    }
}
