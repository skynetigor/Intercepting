using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;

namespace Core.DAL.Models
{
    public class CustomerModel:BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
