using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountDatabase.Entities
{
    [Table("Transactions")]
    public class AccountTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public double PrincipalAmount { get; set; } = 0;
        public double PaidAmount { get; set; } = 0;
        public double BalanceAmount { get; set; } = 0;
        public DateOnly CreatedDate { get; set; }
        public DateOnly UpdatedDate { get; set; }
        public Guid CreatedUserId { get; set; }
        public Guid UpdatedUserId { get; set; }
        public DateOnly StartDate { get; set; } /*EMI Or Intrest Start Date*/
        public DateOnly CloseDate { get; set; }
        public double InterestRate { get; set; }

    }
}
