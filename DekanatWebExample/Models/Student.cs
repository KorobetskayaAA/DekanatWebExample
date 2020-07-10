using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DekanatWebExample.Models
{
    public class Student
    {
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [RegularExpression(@"[1-9]\d+")]
        [StringLength(15, MinimumLength = 4)]
        [Required]
        public string Studbilet { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public string FullName => $"{LastName} {FirstName.Substring(0, 1)}.";

    }
}