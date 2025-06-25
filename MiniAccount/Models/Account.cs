using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniAccount.Models
{
    public class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string AccountName { get; set; }

        [Required]
        public string AccountCode { get; set; }

        [Required]
        public string AccountType { get; set; }

        [Display(Name = "Parent Account")]
        public int? ParentAccountId { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("ParentAccountId")]
        public Account? ParentAccount { get; set; }
    }
}
