using ApiRest_Sitt.Models.Login;

namespace ApiRest_Sitt.Services.Login;

public interface ILoginServices
{
    //Se crea el servicio para devolver el token de autorizacion
    Task<LoginResponse> ReturnToken(LoginRequest autorization);
    Task<LoginResponse> Register(LoginRequest autorization);
}
