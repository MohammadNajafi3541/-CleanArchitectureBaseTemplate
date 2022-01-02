using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
   public class Person
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

    }
}
