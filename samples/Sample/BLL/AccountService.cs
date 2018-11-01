using System.Linq;
using AutoMapper;
using Core.BLL;
using Core.DAL;
using Core.Models;

namespace BLL
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<User> _usersRepository;
        private readonly IMapper _mapper;

        public AccountService(IGenericRepository<User> usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public bool RegisterUser(RegistrationForm registrationForm)
        {
            var user = _mapper.Map<User>(registrationForm);
            _usersRepository.Add(user);

            return true;
        }

        public bool Login(LoginForm user)
        {
            return _usersRepository.GetAll()
                       .FirstOrDefault(t => t.Name == user.UserName && t.Password == user.Password) != null;
        }
    }
}
