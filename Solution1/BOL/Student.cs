using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BOL
{
    public class Student
    {
        [Required(ErrorMessage ="Roll NO is required")]
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string Branch { get; set; }
        public string FatherName { get; set; }

        public DateTime Dob { get; set; }
        public string Gender { get; set; }


    }
}
