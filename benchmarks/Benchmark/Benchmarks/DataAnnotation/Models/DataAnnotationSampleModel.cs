using System.ComponentModel.DataAnnotations;

namespace Benchmark.Benchmarks.DataAnnotation.Models
{
    public class DataAnnotationSampleModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
