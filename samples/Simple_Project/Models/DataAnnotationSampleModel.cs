using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Simple_Project.Models
{
    public class DataAnnotationSampleModel
    {
        [StringLength(10)]
        public string Name { get; set; }

        [Compare("Name")]
        [Required]
        public string Surname { get; set; }

        [Required]
        public string Address { get; set; }

        [Range(10, 20)]
        public int Age { get; set; }
    }
}
