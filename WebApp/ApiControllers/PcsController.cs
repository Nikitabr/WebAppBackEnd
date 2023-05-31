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
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PcsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public PcsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Pcs
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.Pc>), 200 )]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Pc>>> GetPcs()
        {
            var res = (await _bll.Pcs.GetAllAsync())
                .Select(PcMapper.ToPublic).ToList();
            
            return res;
        }


        // GET: api/Pcs/5
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof(App.Public.DTO.v1.Pc), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.Pc>> GetPc(Guid id)
        {
            var pcs = await _bll.Pcs.FirstOrDefaultAsync(id);

            if (pcs == null)
            {
                return NotFound();
            }

            return PcMapper.ToPublic(pcs);
        }

        // PUT: api/Pcs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPc(Guid id, App.Public.DTO.v1.Pc pc)
        {
            if (id != pc.Id)
            {
                return BadRequest();
            }

            var pcFromDb = await _bll.Pcs.FirstOrDefaultAsync(id);
            if (pcFromDb == null)
            {
                return NotFound();
            }

            pcFromDb = PcMapper.ToBll(pc);
            pcFromDb.Description.SetTranslation(pc.Description);
            _bll.Pcs.Update(pcFromDb);
            await _bll.SaveChangesAsync();

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PcExists(id))
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

        // POST: api/Pcs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.Pc>> PostPc([FromBody] App.Public.DTO.v1.Pc pc)
        {
           
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }

            var pcBll = PcMapper.ToBll(pc);
            _bll.Pcs.Add(pcBll);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPc", new
            {
                id = pc.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, pc);
        }

        // DELETE: api/Pcs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePc(Guid id)
        {
            var pc = await _bll.Pcs.FirstOrDefaultAsync(id);
            if (pc == null)
            {
                return NotFound();
            }

            _bll.Pcs.Remove(pc);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> PcExists(Guid id)
        {
            return await _bll.Pcs.ExistsAsync(id);
        }
    }
}
