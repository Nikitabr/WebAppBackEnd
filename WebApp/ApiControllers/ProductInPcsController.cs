#nullable disable
using App.Contracts.BLL;
using App.Public.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductInPcsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ProductInPcsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ProductInPc
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.ProductInPc>), 200 )]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.ProductInPc>>> GetProductInPcs()
        {
            var res = (await _bll.ProductInPcs.GetAllAsync())
                .Select(ProductInPcMapper.ToPublic).ToList();

            return res;
        }

        // GET: api/ProductInPc/5
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof(App.Public.DTO.v1.ProductInPc), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.ProductInPc>> GetProductInPc(Guid id)
        {
            var productInPc = await _bll.ProductInPcs.FirstOrDefaultAsync(id);

            if (productInPc == null)
            {
                return NotFound();
            }

            return ProductInPcMapper.ToPublic(productInPc);
        }

        // PUT: api/ProductInPc/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductInPc(Guid id, App.Public.DTO.v1.ProductInPc productInPc)
        {
            if (id != productInPc.Id)
            {
                return BadRequest();
            }

            var productInPcFromDb = await _bll.ProductInPcs.FirstOrDefaultAsync(id);
            if (productInPcFromDb == null)
            {
                return NotFound();
            }


            productInPcFromDb = ProductInPcMapper.ToBll(productInPc);
            _bll.ProductInPcs.Update(productInPcFromDb);
            await _bll.SaveChangesAsync();
            
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductInPcExists(id))
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

        // POST: api/ProductInPc
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.ProductInPc>> PostProductInPc([FromBody] App.Public.DTO.v1.ProductInPc productInPc)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }

            var productInPcBll = ProductInPcMapper.ToBll(productInPc);
            _bll.ProductInPcs.Add(productInPcBll);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetProductInPc", new { id = productInPc.Id }, productInPc);
        }

        // DELETE: api/ProductInPc/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductInPc(Guid id)
        {
            var productInPc = await _bll.ProductInPcs.FirstOrDefaultAsync(id);
            if (productInPc == null)
            {
                return NotFound();
            }

            _bll.ProductInPcs.Remove(productInPc);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ProductInPcExists(Guid id)
        {
            return await _bll.ProductInPcs.ExistsAsync(id);
        }
    }
}
