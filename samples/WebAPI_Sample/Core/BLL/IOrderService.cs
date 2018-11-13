using System;
using System.Collections.Generic;
using System.Text;
using Core.DAL.Models;

namespace Core.BLL
{
    public interface ICustomerService
    {
        void CreateCustomer(CustomerModel customer);

        void RemoveCustomer(CustomerModel customer);
    }
}
