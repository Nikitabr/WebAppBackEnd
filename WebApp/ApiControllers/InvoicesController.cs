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
using App.Public.DTO.v1;
using App.Public.Mappers;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public InvoicesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Invoice
        /// <summary>
        /// Gets all invoices from the rest backend
        /// </summary>
        /// <returns>All the invoices</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.Invoice>), 200 )]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Invoice>>> GetInvoices()
        {
            var res = (await _bll.Invoices.GetAllAsync())
                .Select(InvoiceMapper.ToPublic).ToList();
            return res;
        }

        // GET: api/Invoice/5
        /// <summary>
        /// Get invoice by id from the rest backend
        /// </summary>
        /// <param name="id">Supply invoice id</param>
        /// <returns>Invoice by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof(App.Public.DTO.v1.Invoice), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.Invoice>> GetInvoice(Guid id)
        {
            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return InvoiceMapper.ToPublic(invoice);
        }

        // PUT: api/Invoice/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Insert new data to invoice found by id, edit invoice in the rest backend 
        /// </summary>
        /// <param name="id">Supply invoice id</param>
        /// <param name="invoice">Supply invoice</param>
        /// <returns>Updated invoice</returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(Guid id, App.Public.DTO.v1.Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest();
            }

            var invoiceFromDb = await _bll.Invoices.FirstOrDefaultAsync(id);
            if (invoiceFromDb == null)
            {
                return NotFound();
            }

            invoiceFromDb = InvoiceMapper.ToBll(invoice);
            _bll.Invoices.Update(invoiceFromDb);
            await _bll.SaveChangesAsync();

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await InvoiceExists(id))
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

        // POST: api/Invoice
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create new invoice in the rest backend
        /// </summary>
        /// <param name="invoice">Supply invoice</param>
        /// <returns>New invoice</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Invoice), StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<App.Public.DTO.v1.Invoice>> PostInvoice([FromBody] App.Public.DTO.v1.Invoice invoice)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }

            var invoiceBll = InvoiceMapper.ToBll(invoice);
            invoiceBll = _bll.Invoices.Add(invoiceBll);
            await _bll.SaveChangesAsync();

            invoice.Id = invoiceBll.Id;
            
            return CreatedAtAction("GetInvoice", new
            {
                id = invoice.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, invoice);
        }

        // DELETE: api/Invoice/5
        /// <summary>
        /// Delete invoice by id from the rest backend
        /// </summary>
        /// <param name="id">Supply invoice id</param>
        /// <returns>Nothing</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _bll.Invoices.Remove(invoice);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> InvoiceExists(Guid id)
        {
            return await _bll.Invoices.ExistsAsync(id);
        }
    }
}
