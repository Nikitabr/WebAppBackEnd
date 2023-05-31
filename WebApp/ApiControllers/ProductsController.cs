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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ProductsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Product
        /// <summary>
        /// Gets all products from the rest backend
        /// </summary>
        /// <returns>All products</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.Product>), 200 )]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Product>>> GetProducts()
        {
            var res = (await _bll.Products.GetAllAsync())
                .Select(ProductMapper.ToPublic).ToList();
            return res;
        }

        // GET: api/Product/5
        
        /// <summary>
        /// Get product by id from the rest backend
        /// </summary>
        /// <param name="id">Supply product id</param>
        /// <returns>Product by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof(App.Public.DTO.v1.Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.Product>> GetProduct(Guid id)
        {
            var product = await _bll.Products.FirstOrDefaultAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return ProductMapper.ToPublic(product);
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Insert new data to product by id, edit product in the rest backend
        /// </summary>
        /// <param name="id">Supply product id</param>
        /// <param name="product">Supply product</param>
        /// <returns>Updated product by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof(App.Public.DTO.v1.Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, App.Public.DTO.v1.Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }


            var productFromDb = await _bll.Products.FirstOrDefaultAsync(id);
            if (productFromDb == null)
            {
                return NotFound();
            }

            productFromDb = ProductMapper.ToBll(product);
            _bll.Products.Update(productFromDb);
            await _bll.SaveChangesAsync();
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExists(id))
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

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Creates new product in the rest backend
        /// </summary>
        /// <param name="product">Supply product</param>
        /// <returns>New product</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.Product>> PostProduct([FromBody] App.Public.DTO.v1.Product product)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }

            var productBll = ProductMapper.ToBll(product);
            _bll.Products.Add(productBll);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new
            {
                id = product.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, product);
        }

        // DELETE: api/Product/5
        /// <summary>
        /// Delete product by id in the rest backend
        /// </summary>
        /// <param name="id">Supply product by id</param>
        /// <returns>Nothing</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _bll.Products.FirstOrDefaultAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _bll.Products.Remove(product);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ProductExists(Guid id)
        {
            return await _bll.Products.ExistsAsync(id);
        }
    }
}
