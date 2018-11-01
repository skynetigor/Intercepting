using System;
using MethodArgsValidationSample.Abstracts;
using MethodArgsValidationSample.Models;
using Newtonsoft.Json;

namespace MethodArgsValidationSample.Services
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

