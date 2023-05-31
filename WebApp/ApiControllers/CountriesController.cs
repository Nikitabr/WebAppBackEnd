using App.Contracts.BLL;
using App.Public.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CountriesController : ControllerBase
    {

        private readonly IAppBLL _bll;

        public CountriesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Country
        /// <summary>
        /// Gets all countries from th rest backend
        /// </summary>
        /// <returns>All the countries</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.Country>), 200 )]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Country>>> GetCountries()
        {

            var res = (await _bll.Countries.GetAllAsync())
                .Select(CountryMapper.ToPublic).ToList();

            
            return res;
        }

        // GET: api/Country/5
        /// <summary>
        /// Get country by Id from the rest backend
        /// </summary>
        /// <param name="id">Supply country Id</param>
        /// <returns>Country by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof(App.Public.DTO.v1.Country), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.Country>> GetCountry(Guid id)
        {
            var country = (await _bll.Countries.FirstOrDefaultAsync(id));

            if (country == null)
            {
                return NotFound();
            }

            return CountryMapper.ToPublic(country);
        }

        // PUT: api/Country/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Insert new data to country by id, edit country in the rest backend
        /// </summary>
        /// <param name="id">Supply country id</param>
        /// <param name="country">Supply country</param>
        /// <returns>Updated country</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(Guid id, App.Public.DTO.v1.Country country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            var countryFromDb = await _bll.Countries.FirstOrDefaultAsync(id);
            if (countryFromDb == null)
            {
                return NotFound();
            }
            
            
            countryFromDb = CountryMapper.ToBll(country);
            countryFromDb.CountryName.SetTranslation(country.CountryName);
            _bll.Countries.Update(countryFromDb);
            await _bll.SaveChangesAsync();

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
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

        // POST: api/Country
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create new country in the rest backend
        /// </summary>
        /// <param name="country">Supply country</param>
        /// <returns>New country</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<App.Public.DTO.v1.Country>> PostCountry([FromBody] App.Public.DTO.v1.Country country)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }
            
            var countryBll = CountryMapper.ToBll(country);
            _bll.Countries.Add(countryBll);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetCountry",
                new
                {
                    id = country.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                country);
        }

        // DELETE: api/Country/5
        /// <summary>
        /// Delete country by id from the rest backend
        /// </summary>
        /// <param name="id">Supply country id</param>
        /// <returns>Nothing</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(Guid id)
        {
            var country = await _bll.Countries.FirstOrDefaultAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _bll.Countries.Remove(country);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
        
        private async Task<bool> CountryExists(Guid id)
        {
            return await _bll.Countries.ExistsAsync(id);
        }
    }
}
