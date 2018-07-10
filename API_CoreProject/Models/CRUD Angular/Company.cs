using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_CoreProject.Models.CRUD_Angular
{
    public class Company
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime FoundedData { get; private set; }
        public IList<Technology> Technologies { get;private set; }

        public Company()
        {
            FoundedData = DateTime.Now;
        }

        public List<Technology> GetTechnologies()
        {
            return Technologies.ToList();
        }

        public void SetTime()
        {
            FoundedData = DateTime.Now;
        }
    }
}
