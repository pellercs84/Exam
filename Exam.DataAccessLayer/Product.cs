using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataAccessLayer
{
    public class Product
    {
        public int ID { get; set; }
        
        [Required, StringLength(100)]
        public string Name { get; set; }
        
        public string ImagePath { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }
        
        public Guid Creator { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime UpdateTime { get; set; }
        
        public Guid Updater { get; set; }

        [ForeignKey("Manufacturer_ID")]
        public virtual Manufacturer Manufacturer { get; set; }
        
        public int Manufacturer_ID { get; set; }
    }
}
