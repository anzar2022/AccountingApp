using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountDatabase.Entities
{
    [Table("InterestEMIs")]
    public  class InterestEMI
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("AccountTransaction")]
        public Guid TransactionId { get; set; }

        public virtual AccountTransaction? AccountTransaction { get; set; }

        public double PrincipalAmount { get; set; }

        public DateOnly GeneratedDate { get; set; }

        public double InterestRate { get; set; }
        public double InterestAmount { get; set; }
        public double BalanceInterestAmount { get; set; }
        public double PaidInterestAmount { get; set; }
        //emi month

        public string EmiMonth { get; set; } 


    }
}






