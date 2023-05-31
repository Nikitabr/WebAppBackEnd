#nullable disable
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Public.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SpecificationTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public SpecificationTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/SpecificationType
        /// <summary>
        /// Get all specificationTypes from the rest backend
        /// </summary>
        /// <returns>All specificationTypes</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.SpecificationType>), 200 )]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.SpecificationType>>> GetSpecificationTypes()
        {
            var res = (await _bll.SpecificationTypes.GetAllAsync())
                .Select(SpecificationTypeMapper.ToPublic)
                .ToList();
            
            return res;
        }

        // GET: api/SpecificationType/5
        /// <summary>
        /// Get specificationType by id from the rest backend
        /// </summary>
        /// <param name="id">Supply specificationType id</param>
        /// <returns>SpecificationType by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof(App.Public.DTO.v1.SpecificationType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.SpecificationType>> GetSpecificationType(Guid id)
        {
            var specificationType = await _bll.SpecificationTypes.FirstOrDefaultAsync(id);

            if (specificationType == null)
            {
                return NotFound();
            }

            return SpecificationTypeMapper.ToPublic(specificationType);
        }

        // PUT: api/SpecificationType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Insert new data to specificationType by id, edit specificationType in the rest backend
        /// </summary>
        /// <param name="id">Supply specificationType id</param>
        /// <param name="specificationType">Supply specificationType</param>
        /// <returns>Updated specificationType by id</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecificationType(Guid id, App.Public.DTO.v1.SpecificationType specificationType)
        {
            if (id != specificationType.Id)
            {
                return BadRequest();
            }

            var specificastionTypeFromDb = await _bll.SpecificationTypes.FirstOrDefaultAsync(id);
            if (specificastionTypeFromDb == null)
            {
                return NotFound();
            }

            specificastionTypeFromDb = SpecificationTypeMapper.ToBll(specificationType);
            specificastionTypeFromDb.Title.SetTranslation(specificationType.Title);
            _bll.SpecificationTypes.Update(specificastionTypeFromDb);
            await _bll.SaveChangesAsync();
            

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SpecificationTypeExists(id))
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

        // POST: api/SpecificationType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create new specificationType in the rest backend
        /// </summary>
        /// <param name="specificationType">Supply specificationType</param>
        /// <returns>New specificationType</returns>
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.SpecificationType>> PostSpecificationType([FromBody] App.Public.DTO.v1.SpecificationType specificationType)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }

            var specificationTypeBll = SpecificationTypeMapper.ToBll(specificationType);
            _bll.SpecificationTypes.Add(specificationTypeBll);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetSpecificationType", new
            {
                id = specificationType.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, 
                specificationType);
        }

        // DELETE: api/SpecificationType/5
        /// <summary>
        /// Delete specificationType by id in the rest backend
        /// </summary>
        /// <param name="id">Supply specificationType id</param>
        /// <returns>Nothing</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecificationType(Guid id)
        {
            var specificationType = await _bll.SpecificationTypes.FirstOrDefaultAsync(id);
            if (specificationType == null)
            {
                return NotFound();
            }

            _bll.SpecificationTypes.Remove(specificationType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> SpecificationTypeExists(Guid id)
        {
            return await _bll.SpecificationTypes.ExistsAsync(id);
        }
    }
}
