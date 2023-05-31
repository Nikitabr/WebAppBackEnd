#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Public.Mappers;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PaymentTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public PaymentTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/PaymentType
        /// <summary>
        /// Gets all payment types from the rest backend
        /// </summary>
        /// <returns>All payment types</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.PaymentType>), 200 )]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.PaymentType>>> GetPaymentTypes()
        {
            var res = (await _bll.PaymentTypes.GetAllAsync())
                .Select(PaymentTypeMapper.ToPublic).ToList();
            return res;
        }

        // GET: api/PaymentType/5
        /// <summary>
        /// Get payment type by id from the rest backend
        /// </summary>
        /// <param name="id">Supply payment type id</param>
        /// <returns>Payment type by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof(App.Public.DTO.v1.PaymentType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.PaymentType>> GetPaymentType(Guid id)
        {
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id);

            if (paymentType == null)
            {
                return NotFound();
            }

            return PaymentTypeMapper.ToPublic(paymentType);
        }

        // PUT: api/PaymentType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Insert new data to payment type by id, edit payment type in the rest backend
        /// </summary>
        /// <param name="id">Supply payment type id</param>
        /// <param name="paymentType">Supply payment type</param>
        /// <returns>Updated payment type by id</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentType(Guid id, App.Public.DTO.v1.PaymentType paymentType)
        {
            if (id != paymentType.Id)
            {
                return BadRequest();
            }

            var paymentTypeFromDb = await _bll.PaymentTypes.FirstOrDefaultAsync(id);
            if (paymentTypeFromDb == null)
            {
                return NotFound();
            }

            paymentTypeFromDb = PaymentTypeMapper.ToBll(paymentType);
            _bll.PaymentTypes.Update(paymentTypeFromDb);
            await _bll.SaveChangesAsync();
            
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PaymentTypeExists(id))
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

        // POST: api/PaymentType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Creates new payment type in the rest backend
        /// </summary>
        /// <param name="paymentType">Supply payment type</param>
        /// <returns>New payment type</returns>
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.PaymentType>> PostPaymentType([FromBody] App.Public.DTO.v1.PaymentType paymentType)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }

            var paymentTypeBll = PaymentTypeMapper.ToBll(paymentType);
            _bll.PaymentTypes.Add(paymentTypeBll);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPaymentType", new
            {
                id = paymentType.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, paymentType);
        }

        // DELETE: api/PaymentType/5
        /// <summary>
        /// Delete payment type in the rest backend
        /// </summary>
        /// <param name="id">Supply payment type id</param>
        /// <returns>Nothing</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentType(Guid id)
        {
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id);
            if (paymentType == null)
            {
                return NotFound();
            }

            _bll.PaymentTypes.Remove(paymentType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> PaymentTypeExists(Guid id)
        {
            return await _bll.PaymentTypes.ExistsAsync(id);
        }
    }
}
