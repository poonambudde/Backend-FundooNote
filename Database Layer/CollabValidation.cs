using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database_Layer
{
    public class CollabValidation
    {
        [Required(ErrorMessage = "{0} should not be empty")]
        [RegularExpression("^[a-z]{3,}[1-9]{1,4}[@][a-z]{4,}[.][a-z]{3,}$", ErrorMessage = "Please Enter Valid Email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

    }
}
