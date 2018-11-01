using System;
using DI.Intercepting.Core.Attributes;
using DI.Intercepting.Repeater.Implementation;
using Newtonsoft.Json;
using Simple_Project.Abstracts;
using Simple_Project.Models;

namespace Simple_Project.Services
{
    public class SomeServiceForSomeModel2 : ISomeServiceForSomeModel2
    {
        public SomeServiceForSomeModel2(ISomeServiceForSomeModel1 service)
        {
            //service.AddSomeModel1(new SomeModel1());
        }

        //[ExcludeFromInterceptingAttribute]
        [Repeat(4)]
        public bool AddSomeModel2(SomeModel2 someModel2, SomeModel2 someModel3, SomeModel2 someModel4, SomeModel2 someModel5, SomeModel2 someModel6)
        {
            Console.WriteLine($"Info from service: SomeModel2 {JsonConvert.SerializeObject(someModel2)} was added");
            return true;
        }

        public int Do()
        {
            return 1;
        }
    }
}
