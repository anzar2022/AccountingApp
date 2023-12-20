using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InterestMasterApi.Entities
{
    public class InterestMaster
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string InterestType { get; set; }

        public int InterestRate { get; set; }
    }
}
