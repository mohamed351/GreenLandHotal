using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GreenLandTesting.Moqs
{
    class EmployeesMoq
    {
    
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal? Salary { get; set; }
    }
}
