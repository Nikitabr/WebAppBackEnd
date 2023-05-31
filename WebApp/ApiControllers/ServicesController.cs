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
    public class ServicesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ServicesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Service
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.Service>), 200 )]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Service>>> GetServices()
        {

            var res = (await _bll.Services.GetAllAsync())
                .Select(ServiceMapper.ToPublic)
                .ToList();
            
            return res;
        }

        // GET: api/Service/5
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof(App.Public.DTO.v1.Service), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.Service>> GetService(Guid id)
        {
            var service = await _bll.Services.FirstOrDefaultAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return ServiceMapper.ToPublic(service);
        }

        // PUT: api/Service/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(Guid id, App.Public.DTO.v1.Service service)
        {
            if (id != service.Id)
            {
                return BadRequest();
            }

            var serviceFromDb = await _bll.Services.FirstOrDefaultAsync(id);
            if (serviceFromDb == null)
            {
                return NotFound();
            }

            serviceFromDb = ServiceMapper.ToBll(service);
            serviceFromDb.Description.SetTranslation(service.Description);
            _bll.Services.Update(serviceFromDb);
            await _bll.SaveChangesAsync();

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ServiceExists(id))
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

        // POST: api/Service
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.Service>> PostService([FromBody] App.Public.DTO.v1.Service service)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }

            var serviceBll = ServiceMapper.ToBll(service);
            _bll.Services.Add(serviceBll);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetService", new
            {
                id = service.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, service);
        }

        // DELETE: api/Service/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            var service = await _bll.Services.FirstOrDefaultAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _bll.Services.Remove(service);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ServiceExists(Guid id)
        {
            return await _bll.Services.ExistsAsync(id);
        }
    }
}
