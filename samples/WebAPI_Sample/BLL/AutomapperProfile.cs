using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Core.Models;

namespace BLL
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<RegistrationForm, User>().ConstructUsing(t => new User()
            {
                Name = t.UserName,
                Email = t.Email,
                Password = t.Password
            });
        }
    }
}
