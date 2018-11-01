using System;
using Newtonsoft.Json;
using Simple_Project.Abstracts;
using Simple_Project.Models;

namespace Simple_Project.Services
{
    public class DataAnnotationSampleService : IDataAnnotationSampleService
    {
        public void Add(DataAnnotationSampleModel model)
        {
            Console.WriteLine($"SomeModel1 {JsonConvert.SerializeObject(model)} was added");
        }

        public DataAnnotationSampleModel Remove(DataAnnotationSampleModel model)
        {
            Console.WriteLine($"SomeModel1 {JsonConvert.SerializeObject(model)} was removed");
            return model;
        }
    }
}

