using BusinessModel;
using System.Threading.Tasks;

namespace Service.Security.JWTService
{
    public interface IJwtService
    {
        Task<string> Generate(UserInfoBusinessModel user);
    }
}