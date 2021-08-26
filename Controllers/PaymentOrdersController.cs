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

        // GET: api/PaymentOrders
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<PaymentOrder>>> GetPaymentOrders()
        //{
        //    return await _context.PaymentOrders.ToListAsync();
        //}

        // GET: api/PaymentOrders/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<PaymentOrder>> GetPaymentOrder(int id)
        //{
        //    var paymentOrder = await _context.PaymentOrders.FindAsync(id);

        //    if (paymentOrder == null)
        //    {
        //        return NotFound();
        //    }

        //    return paymentOrder;
        //}

        // PUT: api/PaymentOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeStatusOfPaymentOrder(int id, string status, string comments)
        {
            PaymentOrder paymentOrder = _context.PaymentOrders.Find(id);
            _context.Entry(paymentOrder).State = EntityState.Modified;
            
            try
            {

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentOrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PaymentOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostPaymentOrder(int id, string status, string comments)
        {

            PaymentOrder paymentOrder = _context.PaymentOrders.Find(id);
            _context.Entry(paymentOrder).State = EntityState.Modified;

            try
            {
                paymentOrder.Status = (int)Enum.Parse(typeof(ApiOrderStatus), status);
                paymentOrder.Reference += "_________/n" + comments;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentOrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
            //return CreatedAtAction("GetPaymentOrder", new { id = paymentOrder.Id }, paymentOrder);
        }

        // DELETE: api/PaymentOrders/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePaymentOrder(int id)
        //{
        //    var paymentOrder = await _context.PaymentOrders.FindAsync(id);
        //    if (paymentOrder == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.PaymentOrders.Remove(paymentOrder);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool PaymentOrderExists(int id)
        {
            return _context.PaymentOrders.Any(e => e.Id == id);
        }

        private PaymentOrder DtoToPaymentOrder(PaymentOrderDTO dto)
        {
            PaymentOrder po = new PaymentOrder
            {
                Id = dto.DboId,
                SenderAccount = dto.PayerAccountNumber,
                ReceiverAccount = dto.ReceiverAccountNumber,
                Sum = dto.Amount,
                Reference = dto.PaymentDetails
            };
            return po;
        }
    }
}
