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
    public class SpecificationsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public SpecificationsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Specification
        /// <summary>
        /// Gets all specifications from the rest backend
        /// </summary>
        /// <returns>All specifications</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.Specification>), 200 )]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Specification>>> GetSpecifications()
        {

            var res = (await _bll.Specifications.GetAllAsync())
                .Select(SpecificationMapper.ToPublic)
                .ToList();
            
            return res;
        }

        // GET: api/Specification/5
        /// <summary>
        /// Get specification by id
        /// </summary>
        /// <param name="id">Supply specification id</param>
        /// <returns>Specification by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof(App.Public.DTO.v1.Specification), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.Specification>> GetSpecification(Guid id)
        {
            var specification = await _bll.Specifications.FirstOrDefaultAsync(id);

            if (specification == null)
            {
                return NotFound();
            }

            return SpecificationMapper.ToPublic(specification);
        }

        // PUT: api/Specification/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Insert new data to specification by id, edit specification in the rest backend
        /// </summary>
        /// <param name="id">Supply specification id</param>
        /// <param name="specification">Supply specification</param>
        /// <returns>Updated specification by id</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecification(Guid id, App.Public.DTO.v1.Specification specification)
        {
            if (id != specification.Id)
            {
                return BadRequest();
            }

            var specificationFromDb = await _bll.Specifications.FirstOrDefaultAsync(id);
            if (specificationFromDb == null)
            {
                return NotFound();
            }

            specificationFromDb = SpecificationMapper.ToBll(specification);
            specificationFromDb.Description.SetTranslation(specification.Description);
            _bll.Specifications.Update(specificationFromDb);
            await _bll.SaveChangesAsync();
            
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SpecificationExists(id))
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

        // POST: api/Specification
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create new specification in the rest backend
        /// </summary>
        /// <param name="specification">Supply specification</param>
        /// <returns>New specification</returns>
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.Specification>> PostSpecification([FromBody] App.Public.DTO.v1.Specification specification)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }

            var specificationBll = SpecificationMapper.ToBll(specification);
            _bll.Specifications.Add(specificationBll);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetSpecification", new
            {
                id = specification.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, 
                specification);
        }

        // DELETE: api/Specification/5
        /// <summary>
        /// Delete specification by id in the rest backend
        /// </summary>
        /// <param name="id">Supply specification by id</param>
        /// <returns>Nothing</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecification(Guid id)
        {
            var specification = await _bll.Specifications.FirstOrDefaultAsync(id);
            if (specification == null)
            {
                return NotFound();
            }

            _bll.Specifications.Remove(specification);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> SpecificationExists(Guid id)
        {
            return await _bll.Specifications.ExistsAsync(id);
        }
    }
}
