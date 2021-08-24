using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PlugApi.Models
{
    [Table("PaymentOrder")]
    public partial class PaymentOrder
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string SenderAccount { get; set; }
        [Required]
        public string ReceiverAccount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Sum { get; set; }
        [Required]
        public string Reference { get; set; }
        public int Status { get; set; }
    }
}
