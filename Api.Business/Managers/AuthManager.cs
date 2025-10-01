using Api.Business.Contracts;
using Api.Models.Common;
using Api.Models.Configuration;
using Api.Models.Response;
using Api.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Api.Business.Response;
using Api.Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;
using Api.Data.AspNet;
using System.Net;
using Api.Models;
using System.Data;
using System.Text;

namespace Api.Business.Managers
{

    public class AuthManager : IAuthManager
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtGenerador _jwtGenerador;
        private readonly IOptions<Settings> _appSettings;
        private readonly ApplicationDbContext _context;

        public AuthManager(IOptions<JwtBearerTokenSettings> jwtTokenOptions, UserManager<IdentityUser> userManager, IJwtGenerador jwtGenerador, IOptions<Settings> appSettings, ApplicationDbContext context)
        {
            this._userManager = userManager;
            _jwtGenerador = jwtGenerador;
            _appSettings = appSettings;
            _context = context;
        }

        //public async Task<ResponseItemDTO<UsuarioDTO>> UserRegister(UserLogin userDetails)
        //{

        //    var errorList = new List<ErrorDTO>();
        //    if (userDetails == null)
        //    {
        //        errorList.Add(new ErrorDTO { Message = "Error al intentar generar el usuario" });
        //        return ResponseData.ResponseFailed<UsuarioDTO>(errorList);
        //    }

        //    var identityUser = new IdentityUser() { UserName = userDetails.Email, Email = userDetails.Email };
        //    var result = await _userManager.CreateAsync(identityUser, userDetails.Password);
        //    if (!result.Succeeded)
        //    {
        //        var error = result.Errors.Select(x => new ErrorDTO { Code = x.Code, Message = x.Description }).ToList();

        //        return ResponseData.ResponseFailed<UsuarioDTO>(error);
        //    }

        //    return ResponseData.ResponseSuccess(UserMap(identityUser));

        //}
        public async Task<ResponseItemDTO<UsuarioDTO>> Login(LoginCredentials credentials)
        {
            var errorList = new List<ErrorDTO>();

            var user = new IdentityUser();

            if (credentials == null
              || (user = await ValidateUser(credentials)) == null)
            {
                errorList.Add(new ErrorDTO { Code="1000", Message = "Usuario y/o incorrecto" });
                return ResponseData.ResponseFailed<UsuarioDTO>(errorList);
            }
            
            UsuarioDTO userData = UserMap(user);
            var token = _jwtGenerador.GenerateToken(userData);
            userData.Token = token?.ToString() ?? string.Empty;

            if (!userData.Activo)
            {
                errorList.Add(new ErrorDTO { Code = "1001", Message = "Usuario inactivo" });
                return ResponseData.ResponseFailed<UsuarioDTO>(errorList);
            }
            return ResponseData.ResponseSuccess(userData);
        }

        //public async Task<ResponseItemDTO<UsuarioDTO>> GetUser(string userName)
        //{
        //    var errorList = new List<ErrorDTO>();

        //    var user = new IdentityUser();

        //    user = await _userManager.FindByNameAsync(userName);

        //    if (user == null)
        //    {
        //        errorList.Add(new ErrorDTO { Message = "Usuario y/o incorrecto" });
        //        return ResponseData.ResponseFailed<UsuarioDTO>(errorList);
        //    }

        //    UsuarioDTO userData = UserMap(user);
        //    return ResponseData.ResponseSuccess(userData);
        //}

        //public async Task<ResponseItemDTO<UsuarioData>> UpdatePhoneUser(UsuarioData request)
        //{
        //    var error = new ErrorDTO { Code = "1002", Message = "Error al intentar actualizar el usuario" };

        //    try
        //    {
        //        if (request == null)
        //        {
        //            return ResponseData.ResponseFailed<UsuarioData>(error);
        //        }

        //        var user = await _userManager.FindByNameAsync(request.Email);
        //        user.PhoneNumber = request.PhoneNumber;

        //        var resultado = await _userManager.UpdateAsync(user);

        //        return resultado != null ? ResponseData.ResponseSuccess(request) : ResponseData.ResponseFailed<UsuarioData>(error);
        //    }
        //    catch (Exception ex)
        //    {

        //        return ResponseData.ResponseFailed<UsuarioData>(error);
        //    }

        //}

        public async Task<ResponseItemDTO<UserLogin>> UpdatePasswordUser(UserLogin request)
        {
            var error = new ErrorDTO { Code = "1003", Message = "Error al intentar actualizar el usuario" };

            try
            {
                if (request == null)
                {
                    return ResponseData.ResponseFailed<UserLogin>(error);
                }
                IdentityResult? resultado = null;
                var user = await _userManager.FindByNameAsync(request.Email);

                var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

                //                var tmpPwd = _userManager.PasswordHasher.HashPassword(user, request.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.ConfirmPassword);
                    resultado = await _userManager.UpdateAsync(user);
                }
                else
                {
                    error.Message = "Contraseña incorrecta del usuario";
                }


                return resultado != null ? ResponseData.ResponseSuccess(request) : ResponseData.ResponseFailed<UserLogin>(error);
            }
            catch (Exception ex)
            {

                return ResponseData.ResponseFailed<UserLogin>(error);
            }

        }

        private async Task<UsuarioDTO> ValidateUser(LoginCredentials credentials)
        {
            UsuarioDTO? user = new UsuarioDTO();
            if (credentials != null)
            {
                var parametros = new SqlParameter[]{
                    new("UserName", credentials.Email),
                    new("Password", EncriptaStringAES(credentials.Password)),
                    new("Cliente", credentials.ClienteId)
                };
                var result = _context.Users.FromSqlRaw("EXEC GetUser @UserName,@Password,@Cliente ", parametros).ToList();

                user = !result.Any() ? null : result.Select(x => new UsuarioDTO
                {
                    UserName = x.UserName, 
                    Email = x.Email,
                    UsuarioId= x.UsuarioId,
                    NombreCompleto =x.NombreCompleto,
                    PerfilId = x.PerfilId,
                    PerfilNombre = x.PerfilNombre,
                    Activo = x.Activo,
                    EscuelaId = x.EscuelaId,
                    EscuelaNombre =x.EscuelaNombre,
                    EscuelaLatitud =x.EscuelaLatitud,
                    EscuelaLongitud =x.EscuelaLongitud,
                    ImplementacionId =x.ImplementacionId,
                    TokenDispositivo =x.TokenDispositivo
                }).First();
                if (user != null)
                {
                    switch (user.PerfilNombre.ToUpper())
                    {
                        case "ALUMNO":
                        case "TUTOR":
                            var param = new SqlParameter[]{
                                new("UserId", user.UsuarioId),
                                new("Cliente", credentials.ClienteId)
                            };
                            var resultAlumnos = _context.Alumnos.FromSqlRaw("EXEC GetAlumnos @UserId,@Cliente ", param).ToList();
                            List<AlumnoDTO> ListaAlumno = new List<AlumnoDTO>();

                            foreach (AlumnoDTO vItem in resultAlumnos)
                            {
                                ListaAlumno.Add(vItem);
                            }
                            user.ListaAlumnos = ListaAlumno;
                            break;
                        case "PROFESOR":
                            break;
                    }
                    var response = new List<RoleViewDTO>();
                    var viewList = await _context.Views.ToListAsync();
                    var viewParent = viewList.Where(x => x.ParentId != null).Select(x => x.ParentId).Distinct();
                    var views = viewList.Where(x => viewParent.All(p => p != x.ViewId)).Select(x => x);
                    var ViewRol = await _context.ViewRols.Where(x => x.Roles.Name.ToUpper() == user.PerfilNombre.ToUpper()).Select(x => x).ToListAsync();

                    response = (from v in views
                                join vr in ViewRol
                                   on v.ViewId equals vr.Views.ViewId into grouping
                                from vlist in grouping.DefaultIfEmpty()

                                select new RoleViewDTO
                                {
                                    RolesId = vlist != null ? vlist.RolesId : user.PerfilId.ToString(),
                                    IsAdd = vlist != null ? vlist.IsAdd : false,
                                    IsView = vlist != null ? vlist.IsView : false,
                                    IsUpdate = vlist != null ? vlist.IsUpdate : false,
                                    IsDelete = vlist != null ? vlist.IsDelete : false,
                                    Views = new ViewDTO { Description = v.Description, ViewId = v.ViewId },
                                    Parent = v.Parent != null ? new ViewDTO { Description = v.Parent.Description, ViewId = v.Parent.ViewId } : new ViewDTO()
                                }).ToList();
                    user.ListaRolView = response;
                }
                              
            }

            return user;
        }
        private UsuarioDTO UserMap(IdentityUser userIdentity)
        {
            var serializedParent = JsonConvert.SerializeObject(userIdentity);
            return JsonConvert.DeserializeObject<UsuarioDTO>(serializedParent) ?? new UsuarioDTO();
        }
        private static string EncriptaStringAES(string valor)
        {
            string palabraPaso = "GaqpWvxaLKrAsSkeKomP9MAxX9BQWTfrEn27Rud+/aZ+C863NcoSrcHixqExh9H72UMVzG8+nL8DvL9u";
            string valorRGBSalt = "uKFTJbhr3bgmTu4yT6mmnBct4xP4qkypTUzaNr+qT7eENuSCfGilOu3Our2nT09WZE1QwV1jAalFndDW";
            string algoritmoEncriptacionHASH = "MD5";
            int iteraciones = 22;
            string vectorInicial = "cBP5zBua/uRKqVPc";
            int tamanoClave = 128;

            try
            {
                byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(vectorInicial);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(valorRGBSalt);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(valor);

                PasswordDeriveBytes password =
                    new PasswordDeriveBytes(palabraPaso, saltValueBytes,
                        algoritmoEncriptacionHASH, iteraciones);

                byte[] keyBytes = password.GetBytes(tamanoClave / 8);

                RijndaelManaged symmetricKey = new RijndaelManaged();

                symmetricKey.Mode = CipherMode.CBC;

                ICryptoTransform encryptor =
                    symmetricKey.CreateEncryptor(keyBytes, InitialVectorBytes);

                MemoryStream memoryStream = new MemoryStream();

                CryptoStream cryptoStream =
                    new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                cryptoStream.FlushFinalBlock();

                byte[] cipherTextBytes = memoryStream.ToArray();

                memoryStream.Close();
                cryptoStream.Close();

                string textoCifradoFinal = Convert.ToBase64String(cipherTextBytes);

                return textoCifradoFinal;
            }
            catch
            {
                return null;
            }
        }


    }
}
