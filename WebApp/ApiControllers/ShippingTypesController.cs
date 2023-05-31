#nullable disable
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain;
using App.Public.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ShippingTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ShippingTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ShippingType
        /// <summary>
        /// Gets all shippingType from the rest backend
        /// </summary>
        /// <returns>All shippingTypes</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.ShippingType>), 200 )]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.ShippingType>>> GetShippingTypes()
        {
            var res = (await _bll.ShippingTypes.GetAllAsync())
                .Select(ShippingTypeMapper.ToPublic)
                .ToList();
            
            return res;
        }

        // GET: api/ShippingType/5
        /// <summary>
        /// Get shippingType by id from the rest backend
        /// </summary>
        /// <param name="id">Supply shippingType id</param>
        /// <returns>ShippingType by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof(App.Public.DTO.v1.ShippingType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.ShippingType>> GetShippingType(Guid id)
        {
            var shippingType = await _bll.ShippingTypes.FirstOrDefaultAsync(id);

            if (shippingType == null)
            {
                return NotFound();
            }

            return ShippingTypeMapper.ToPublic(shippingType);
        }

        // PUT: api/ShippingType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Insert new data to shippingType by id, edit shippingType in the rest backend
        /// </summary>
        /// <param name="id">Supply shippingType id</param>
        /// <param name="shippingType">Supply shippingType</param>
        /// <returns>Updated ShippingType by id</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShippingType(Guid id, App.Public.DTO.v1.ShippingType shippingType)
        {
            if (id != shippingType.Id)
            {
                return BadRequest();
            }

            var shippingTypeFromDb = await _bll.ShippingTypes.FirstOrDefaultAsync(id);
            if (shippingTypeFromDb == null)
            {
                return NotFound();
            }

            shippingTypeFromDb = ShippingTypeMapper.ToBll(shippingType);
            shippingTypeFromDb.Title.SetTranslation(shippingType.Title);
            _bll.ShippingTypes.Update(shippingTypeFromDb);
            await _bll.SaveChangesAsync();
            
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ShippingTypeExists(id))
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

        // POST: api/ShippingType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create new shippingType in the rest backend
        /// </summary>
        /// <param name="shippingType">Supply shippingType</param>
        /// <returns>New shippingType</returns>
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.ShippingType>> PostShippingType([FromBody] App.Public.DTO.v1.ShippingType shippingType)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }

            var shippingTypeBll = ShippingTypeMapper.ToBll(shippingType);
            _bll.ShippingTypes.Add(shippingTypeBll);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetShippingType", new
            {
                id = shippingType.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            },
                shippingType);
        }

        // DELETE: api/ShippingType/5
        /// <summary>
        /// Delete shippingType by id in the rest backend
        /// </summary>
        /// <param name="id">Supply shippingType id</param>
        /// <returns>Nothing</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippingType(Guid id)
        {
            var shippingType = await _bll.ShippingTypes.FirstOrDefaultAsync(id);
            if (shippingType == null)
            {
                return NotFound();
            }

            _bll.ShippingTypes.Remove(shippingType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ShippingTypeExists(Guid id)
        {
            return await _bll.ShippingTypes.ExistsAsync(id);
        }
    }
}
