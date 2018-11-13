using System.ComponentModel.DataAnnotations;

namespace MethodArgsValidationSample.Models
{
    public class DataAnnotationSampleModel
    {
        [Required]
        public string Name { get; set; }

        public string Surname { get; set; }

        [Required]
        public string Address { get; set; }

        [Range(10, 20)]
        public int Age { get; set; }
    }
}
