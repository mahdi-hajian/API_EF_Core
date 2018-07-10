using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_CoreProject.Models.CRUD_Angular
{
    public class Technology
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime Appended { get; private set; }
        public long CompanyID { get; set; }

        public Company Company { get;private set; }
        public Technology()
        {
            Appended = DateTime.Now;
        }

        public Company GetCompany()
        {
            return Company;
        }

        public void SetTime()
        {
            Appended = DateTime.Now;
        }
    }
}
