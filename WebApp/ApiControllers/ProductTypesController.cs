#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain;
using App.Public.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ProductTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ProductType
        /// <summary>
        /// Gets all productTypes from the rest backend
        /// </summary>
        /// <returns>All productTypes</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.ProductType>), 200 )]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.ProductType>>> GetProductTypes()
        {
            var res = (await _bll.ProductTypes.GetAllAsync())
                .Select(ProductTypeMapper.ToPublic)
                .ToList();
            
            return res;
        }

        // GET: api/ProductType/5
        /// <summary>
        /// Get productType by id from the rest backend
        /// </summary>
        /// <param name="id">Supply productType id</param>
        /// <returns>ProductType by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof(App.Public.DTO.v1.ProductType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.ProductType>> GetProductType(Guid id)
        {
            var productType = await _bll.ProductTypes.FirstOrDefaultAsync(id);

            if (productType == null)
            {
                return NotFound();
            }

            return ProductTypeMapper.ToPublic(productType);
        }

        // PUT: api/ProductType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Insert new data to productType by id, edit productType in the rest backend
        /// </summary>
        /// <param name="id">Supply productType id</param>
        /// <param name="productType">Supply productType</param>
        /// <returns>Updated productType by id</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductType(Guid id, App.Public.DTO.v1.ProductType productType)
        {
            if (id != productType.Id)
            {
                return BadRequest();
            }

            var productTypeFromDb = await _bll.ProductTypes.FirstOrDefaultAsync(id);
            if (productTypeFromDb == null)
            {
                return NotFound();
            }

            productTypeFromDb = ProductTypeMapper.ToBll(productType);
            productTypeFromDb.Title.SetTranslation(productType.Title);
            _bll.ProductTypes.Update(productTypeFromDb);
            await _bll.SaveChangesAsync();
            

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductTypeExists(id))
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

        // POST: api/ProductType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Creates new productType in the rest backend
        /// </summary>
        /// <param name="productType">Supply productType</param>
        /// <returns>New productType</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.ProductType>> PostProductType([FromBody] App.Public.DTO.v1.ProductType productType)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }

            var productTypeBll = ProductTypeMapper.ToBll(productType);
            _bll.ProductTypes.Add(productTypeBll);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetProductType", new
            {
                id = productType.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, productType);
        }

        // DELETE: api/ProductType/5
        /// <summary>
        /// Delete productType by id from the rest backend
        /// </summary>
        /// <param name="id">Supply productType id</param>
        /// <returns>Nothing</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductType(Guid id)
        {
            var productType = await _bll.ProductTypes.FirstOrDefaultAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            _bll.ProductTypes.Remove(productType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ProductTypeExists(Guid id)
        {
            return await _bll.ProductTypes.ExistsAsync(id);
        }
    }
}
