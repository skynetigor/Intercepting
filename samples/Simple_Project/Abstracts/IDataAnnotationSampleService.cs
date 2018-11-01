using System;
using System.Collections.Generic;
using System.Text;
using Simple_Project.Models;

namespace Simple_Project.Abstracts
{
    public interface IDataAnnotationSampleService
    {
        void Add(DataAnnotationSampleModel model);

        DataAnnotationSampleModel Remove(DataAnnotationSampleModel model);
    }
}
