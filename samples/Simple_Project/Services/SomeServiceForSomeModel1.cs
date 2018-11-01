using System;
using Newtonsoft.Json;
using Simple_Project.Abstracts;
using Simple_Project.Models;

namespace Simple_Project.Services
{
    public class SomeServiceForSomeModel1 : ISomeServiceForSomeModel1
    {
        public void AddSomeModel1(SomeModel1 someModel1)
        {
            Console.WriteLine($"SomeModel1 {JsonConvert.SerializeObject(someModel1)} was added");
        }
    }
}
