#nullable disable
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain;
using App.Public.Mappers;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ShippingInfosController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ShippingInfosController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ShippingInfo
        /// <summary>
        /// Gets all shippingInfos from the rest backend
        /// </summary>
        /// <returns>All shippingInfos</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.ShippingInfo>), 200 )]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.ShippingInfo>>> GetShippingInfos()
        {
            var res = (await _bll.ShippingInfos.GetAllAsync())
                .Select(ShippingInfoMapper.ToPublic).ToList();
            
            return res;
        }

        // GET: api/ShippingInfo/5
        /// <summary>
        /// Get shippingInfo by id from the rest backend
        /// </summary>
        /// <param name="id">Supply shippingInfo id</param>
        /// <returns>ShippingInfo by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof(App.Public.DTO.v1.ServiceType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.ShippingInfo>> GetShippingInfo(Guid id)
        {
            var shippingInfo = await _bll.ShippingInfos.FirstOrDefaultAsync(id);

            if (shippingInfo == null)
            {
                return NotFound();
            }

            return ShippingInfoMapper.ToPublic(shippingInfo);
        }

        // PUT: api/ShippingInfo/5
        // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Insert new data to shippingInfo by id, edit shippingInfo in the rest backend
        /// </summary>
        /// <param name="id">Supply shippingInfo id</param>
        /// <param name="shippingInfo">Supply shippingInfo</param>
        /// <returns>Updated shippingInfo by id</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShippingInfo(Guid id, App.Public.DTO.v1.ShippingInfo shippingInfo)
        {
            if (id != shippingInfo.Id)
            {
                return BadRequest();
            }

            var shippingInfoFromDb = await _bll.ShippingInfos.FirstOrDefaultAsync(id);
            if (shippingInfoFromDb == null)
            {
                return NotFound();
            }

            shippingInfoFromDb = ShippingInfoMapper.ToBll(shippingInfo);
            _bll.ShippingInfos.Update(shippingInfoFromDb);
            await _bll.SaveChangesAsync();
            
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ShippingInfoExists(id))
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

        // POST: api/ShippingInfo
        // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Creates new shippingInfo in the rest backend
        /// </summary>
        /// <param name="shippingInfo">Supply shippingInfo</param>
        /// <returns>New shippingInfo</returns>
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.ShippingInfo>> PostShippingInfo([FromBody] App.Public.DTO.v1.ShippingInfo shippingInfo)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }

            var shippingInfoBll = ShippingInfoMapper.ToBll(shippingInfo); 
            shippingInfoBll = _bll.ShippingInfos.Add(shippingInfoBll);
            await _bll.SaveChangesAsync();

            shippingInfo.Id = shippingInfoBll.Id;
            
            return CreatedAtAction("GetShippingInfo", new
            {
                id = shippingInfo.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, 
                shippingInfo);
        }

        // DELETE: api/ShippingInfo/5
        /// <summary>
        /// Delete shippingInfo by id in the rest backend
        /// </summary>
        /// <param name="id">Supply shippingInfo id</param>
        /// <returns>Nothing</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippingInfo(Guid id)
        {
            var shippingInfo = await _bll.ShippingInfos.FirstOrDefaultAsync(id);
            if (shippingInfo == null)
            {
                return NotFound();
            }

            _bll.ShippingInfos.Remove(shippingInfo);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ShippingInfoExists(Guid id)
        {
            return await _bll.ShippingInfos.ExistsAsync(id);
        }
    }
}
