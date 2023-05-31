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
using App.Domain;
using App.Public.Mappers;
using Microsoft.AspNetCore.Authorization;
using ProductInOrder = App.Public.DTO.v1.ProductInOrder;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductInOrderController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ProductInOrderController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ProductInOrder
        /// <summary>
        /// Gets all productInOrder from the rest backend
        /// </summary>
        /// <returns>All productInOrder </returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.ProductInOrder>), 200 )]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.ProductInOrder>>> GetProductInOrders()
        {
            var res = (await _bll.ProductInOrders.GetAllAsync())
                .Select(ProductInOrderMapper.ToPublic).ToList();
            return res;
        }

        // GET: api/ProductInOrder/5
        /// <summary>
        /// Get product in order by id from the rest backend
        /// </summary>
        /// <param name="id">Supply product in order id</param>
        /// <returns>Product in order by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof(App.Public.DTO.v1.ProductInOrder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.ProductInOrder>> GetProductInOrder(Guid id)
        {
            var productInOrder = await _bll.ProductInOrders.FirstOrDefaultAsync(id);

            if (productInOrder == null)
            {
                return NotFound();
            }

            return ProductInOrderMapper.ToPublic(productInOrder);
        }

        // PUT: api/ProductInOrder/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Insert new data to productInOrder by id, edit productInOrder in the rest backend
        /// </summary>
        /// <param name="id">Supply productInOrder id</param>
        /// <param name="productInOrder">Supply productInOrder</param>
        /// <returns>Updated productInOrder by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof(App.Public.DTO.v1.Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductInOrder(Guid id, App.Public.DTO.v1.ProductInOrder productInOrder)
        {
            if (id != productInOrder.Id)
            {
                return BadRequest();
            }

            var productInOrderFromDb = await _bll.ProductInOrders.FirstOrDefaultAsync(id);
            if (productInOrderFromDb == null)
            {
                return NotFound();
            }

            productInOrderFromDb = ProductInOrderMapper.ToBll(productInOrder);
            _bll.ProductInOrders.Update(productInOrderFromDb);
            await _bll.SaveChangesAsync();

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductInOrderExists(id))
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

        // POST: api/ProductInOrder
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Creates new productInOrder in the rest backend
        /// </summary>
        /// <param name="productInOrder">Supply productInOrder</param>
        /// <returns>New productInOrder</returns>
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.ProductInOrder>> PostProductInOrder([FromBody] ProductInOrder productInOrder)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }

            var productInOrderBll = ProductInOrderMapper.ToBll(productInOrder);
            _bll.ProductInOrders.Add(productInOrderBll);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetProductInOrder", new
            {
                id = productInOrder.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, productInOrder);
        }

        // DELETE: api/ProductInOrder/5
        /// <summary>
        /// Delete productInOrder by id in the rest backend
        /// </summary>
        /// <param name="id">Supply productInOrder id</param>
        /// <returns>Nothing</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductInOrder(Guid id)
        {
            var productInOrder = await _bll.ProductInOrders.FirstOrDefaultAsync(id);
            if (productInOrder == null)
            {
                return NotFound();
            }

            _bll.ProductInOrders.Remove(productInOrder);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ProductInOrderExists(Guid id)
        {
            return await _bll.ProductInOrders.ExistsAsync(id);
        }
    }
}
