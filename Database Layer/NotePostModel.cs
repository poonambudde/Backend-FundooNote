using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database_Layer
{
    public class NotePostModel
    {
        [Required(ErrorMessage = "Title is required.")]
        [RegularExpression("^[A-Z][a-z]{4,}", ErrorMessage = "Title should start with capital letters and" +
             "should contain atleast 4 characters.")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [RegularExpression("^[A-Z][a-z]{4,}", ErrorMessage = "Description should start with capital letters and" +
            "should contain atleast 4 characters.")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        public string BGColour { get; set; }

        [Required]
        public bool IsArchive { get; set; }

        [Required]
        public bool IsReminder { get; set; }

        [Required]
        public bool IsPin { get; set; }

        [Required]
        public bool IsTrash { get; set; }
    }
}
