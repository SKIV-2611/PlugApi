using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlugApi.Models
{
    public class PaymentOrderDTO
    {
        public int DboId { get; set; }

        public string PayerAccountNumber { get; set; }

        public string ReceiverAccountNumber { get; set; }

        public decimal Amount { get; set; }

        public string PaymentDetails { get; set; }
    }
}
