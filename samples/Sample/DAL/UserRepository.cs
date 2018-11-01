using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DAL;
using Core.Models;

namespace DAL
{
    class UserRepository : IGenericRepository<User>
    {
        private static readonly List<User> Users = new List<User>();

        public void Add(User model)
        {
            Users.Add(model);
        }

        public bool Remove(User model)
        {
            return Users.Remove(model);
        }

        public IEnumerable<User> GetAll()
        {
            return Users.Select(t => new User
            {
                Name = t.Name,
                Email = t.Email,
                Password = t.Password
            }).ToArray();
        }

        public User GetById(int id)
        {
            return Users.Find(t => t.Id == id);
        }
    }
}
