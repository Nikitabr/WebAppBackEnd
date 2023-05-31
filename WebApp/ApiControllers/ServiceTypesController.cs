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
    public class ServiceTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ServiceTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ServiceType
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.ServiceType>), 200 )]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.ServiceType>>> GetServiceTypes()
        {
            var res = (await _bll.ServiceTypes.GetAllAsync())
                .Select(ServiceTypeMapper.ToPublic)
                .ToList();
            
            return res;
        }

        // GET: api/ServiceType/5
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof(App.Public.DTO.v1.ServiceType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.ServiceType>> GetServiceType(Guid id)
        {
            var serviceType = await _bll.ServiceTypes.FirstOrDefaultAsync(id);

            if (serviceType == null)
            {
                return NotFound();
            }

            return ServiceTypeMapper.ToPublic(serviceType);
        }

        // PUT: api/ServiceType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceType(Guid id, App.Public.DTO.v1.ServiceType serviceType)
        {
            if (id != serviceType.Id)
            {
                return BadRequest();
            }

            var serviceTypeFromDb = await _bll.ServiceTypes.FirstOrDefaultAsync(id);
            if (serviceTypeFromDb == null)
            {
                return NotFound();
            }

            serviceTypeFromDb = ServiceTypeMapper.ToBll(serviceType);
            
            serviceTypeFromDb.Title.SetTranslation(serviceType.Title);
            _bll.ServiceTypes.Update(serviceTypeFromDb);
            await _bll.SaveChangesAsync();

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ServiceTypeExists(id))
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

        // POST: api/ServiceType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.ServiceType>> PostServiceType([FromBody] App.Public.DTO.v1.ServiceType serviceType)
        {
            
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }

            var serviceTypeBll = ServiceTypeMapper.ToBll(serviceType);
            _bll.ServiceTypes.Add(serviceTypeBll);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetServiceType", new { id = serviceType.Id }, serviceType);
        }

        // DELETE: api/ServiceType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceType(Guid id)
        {
            var serviceType = await _bll.ServiceTypes.FirstOrDefaultAsync(id);
            if (serviceType == null)
            {
                return NotFound();
            }

            _bll.ServiceTypes.Remove(serviceType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ServiceTypeExists(Guid id)
        {
            return await _bll.ServiceTypes.ExistsAsync(id);
        }
    }
}
