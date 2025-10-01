using Api.Models;
using Api.Models.Response;
using Api.Models.User;

namespace Api.Business.Contracts
{
    public interface IAuthManager
    {
        //Task<ResponseItemDTO<UsuarioData>> UserRegister(UserLogin userLogin);

        Task<ResponseItemDTO<UsuarioDTO>> Login(LoginCredentials credentials);

        //Task<ResponseItemDTO<UsuarioData>> GetUser(string userName);

        //Task<ResponseItemDTO<UsuarioData>> UpdatePhoneUser(UsuarioData request);

        Task<ResponseItemDTO<UserLogin>> UpdatePasswordUser(UserLogin request);
    }
}
