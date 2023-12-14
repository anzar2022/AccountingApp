using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountDatabase.Entities
{
    [Table("Mobiles")]
    public class Mobile
    {
        [Key]
        public int Id { get; set; }
    }
}
