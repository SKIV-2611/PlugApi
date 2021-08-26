using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlugApi.Models;

namespace PlugApi.Controllers
{
    [Route("api/PlugPaymentOrders")]
    [ApiController]
    public class PaymentOrdersController : ControllerBase
    {
        private readonly DBS_apiContext _context;

        public PaymentOrdersController(DBS_apiContext context)
        {
            _context = context;
        }

        
        // POST: api/PaymentOrders
        [HttpPost]
        public async Task<ActionResult> PostPaymentOrder(int id, string status, string comments)
        {

            PaymentOrder paymentOrder = _context.PaymentOrders.Find(id);
            _context.Entry(paymentOrder).State = EntityState.Modified;

        
            paymentOrder.Status = (int)Enum.Parse(typeof(ApiOrderStatus), status);
            //paymentOrder.Reference += "_________/n" + comments;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
