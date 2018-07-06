﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Models.CRUD_Angular
{
    public class Company
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime FoundedData { get; set; }
        public ICollection<Technology> Technologies { get; set; }

        public Company()
        {
            FoundedData = DateTime.Now;
        }
    }
}