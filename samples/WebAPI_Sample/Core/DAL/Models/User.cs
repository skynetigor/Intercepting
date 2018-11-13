using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class User: BaseModel
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
