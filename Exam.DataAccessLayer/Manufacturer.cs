using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam.DataAccessLayer
{
    public class Manufacturer
    {
        public int ID { get; set; }
        
        [Required,StringLength(100)]
        public string Name { get; set; }
        
        public string ImagePath { get; set; }
        
        [Column(TypeName = "datetime2")]
        
        public DateTime CreationTime { get; set; }
        
        public Guid Creator { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime UpdateTime { get; set; }
        
        public Guid Updater { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }
    }

    
}
