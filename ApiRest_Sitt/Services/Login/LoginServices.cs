using ApiRest_Sitt.Data;
using ApiRest_Sitt.Models.Login;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace ApiRest_Sitt.Services.Login;

public class LoginServices : ILoginServices
{
    //Se crean variables para el guardado de la configuracion y el uso del Contexto de la base de datos
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    //Se crea un constructor para retornar la informacion en las variables
    public LoginServices(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<LoginResponse> Register(LoginRequest autorization)
    {
        Models.Users users = new Models.Users();
        users.Username = autorization.Username;
        users.Password = autorization.Password;

        _context.Users.Add(users);
        await _context.SaveChangesAsync();
        return new LoginResponse() { Token = "", response = true, message = "Ok" };
    }

    public async Task<LoginResponse> ReturnToken(LoginRequest login)
    {
        //Se busca y valida que exista un usuario con Email y pass en la DB
        var user = _context.Users.FirstOrDefault(x =>
            x.Username == login.Username &&
            x.Password == login.Password
            );

        if (user == null)
        {
            return await Task.FromResult<LoginResponse>(null);
        }
        //Se llama al metodo que genera el token
        string token = GetToken(user.UserId.ToString());

        return new LoginResponse() { Token = token, response = true, message = "Ok" };

    }
    private string GetToken(string idUser)
    {
        //Se obtiene la clave guardada en el AppSettings.Json y se crea un arreglo de bytes usandola
        var key = _configuration.GetValue<string>("JwtSettings:key");
        var keyByte = Encoding.ASCII.GetBytes(key);

        var request = new ClaimsIdentity();
        request.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUser));

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(keyByte),
            SecurityAlgorithms.HmacSha256Signature);

        var token = new SecurityTokenDescriptor
        {
            Subject = request,
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = credentials
        };

        //Se crea el token usando las configuraciones
        var handler = new JwtSecurityTokenHandler();
        var tokenConf = handler.CreateToken(token);

        string returnToken = handler.WriteToken(tokenConf);

        return returnToken;
    }
}
