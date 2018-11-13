using System.Linq;
using Core.BLL;
using Core.Models;
using DI.ValidationInterceptor;
using Microsoft.AspNetCore.Mvc;

namespace API.First.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginForm value)
        {
            try
            {
                _accountService.Login(value);
                return Ok();
            }
            catch (MethodArgsValidationException e)
            {
                return this.StatusCode(500 , new
                {
                    methodName = e.Method.Name,
                    Errors = e.Errors.Select(t => new { Errors = t.Errors.Select(c => new { PropertyName = c.PropertyName, Errors = c.Errors }) })
                });
            }
        }

        [HttpPost]
        public IActionResult CreateAccount([FromBody] RegistrationForm value)
        {
            try
            {
                _accountService.RegisterUser(value);
                return Ok();
            }
            catch (MethodArgsValidationException e)
            {
                return this.StatusCode(500, new
                {
                    methodName = e.Method.Name,
                    Errors = e.Errors.Select(t => new { Errors = t.Errors.Select(c => new { PropertyName = c.PropertyName, Errors = c.Errors }) })
                });
            }
        }
    }
}
