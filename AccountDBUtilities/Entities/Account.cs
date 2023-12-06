using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AccountDBUtilities.Entities
{

    [Table("Accounts")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
