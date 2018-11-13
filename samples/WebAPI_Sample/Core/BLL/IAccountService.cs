using Core.Models;

namespace Core.BLL
{
    public interface IAccountService
    {
        bool RegisterUser(RegistrationForm user);

        bool Login(LoginForm user);
    }
}
