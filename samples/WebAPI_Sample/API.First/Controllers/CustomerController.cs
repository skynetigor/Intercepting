using Core.BLL;
using Core.DAL.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.First.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController: ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody]CustomerModel customer)
        {
            try
            {
                _customerService.CreateCustomer(customer);
                return Ok();
            }
            catch (ValidationException e)
            {
                return StatusCode(500, new
                {
                    errors = e.Errors,
                    message = e.Message,
                    data = e.Data
                });
            }
        }

        [HttpPost]
        public IActionResult RemoveCustomer([FromBody]CustomerModel customer)
        {
            try
            {
                _customerService.RemoveCustomer(customer);
                return Ok();
            }
            catch (ValidationException e)
            {
                return StatusCode(500, new
                {
                    errors = e.Errors,
                    message = e.Message,
                    data = e.Data
                });
            }
        }
    }
}
