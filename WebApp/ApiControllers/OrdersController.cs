#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Identity;
using App.Public.DTO.v1;
using App.Public.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NuGet.Versioning;

namespace WebApp.ApiControllers
{
    
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        private readonly UserManager<App.Domain.Identity.AppUser> _userManager;

        public OrdersController(IAppBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: api/Order
        /// <summary>
        /// Gets all orders from the rest backend
        /// </summary>
        /// <returns>All orders</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.Order>), 200 )]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Order>>> GetOrders()
        {
            
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity.Claims.ToList();
            if (identity.HasClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "admin"))
            {
                return (await _bll.Orders.GetAllAsync())
                    .Select(OrderMapper.ToPublic).ToList();
            }
            return (await _bll.Orders.GetAllOrdersByUserId(claims[0].Value))
                .Select(OrderMapper.ToPublic).ToList();
            // return (await _bll.Orders.GetAllAsync()).Select(OrderMapper.ToPublic).ToList();
        }

        // GET: api/Order/5
        /// <summary>
        /// Get order by id from the rest backend
        /// </summary>
        /// <param name="id">Supply order id</param>
        /// <returns>Order by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof(App.Public.DTO.v1.Invoice), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.Order>> GetOrder(Guid id)
        {
            var order = await _bll.Orders.FirstOrDefaultAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return OrderMapper.ToPublic(order);
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Insert new data to order by id, edit order in the rest backend
        /// </summary>
        /// <param name="id">Supply order id</param>
        /// <param name="order">Supply order</param>
        /// <returns>Updated order</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, App.Public.DTO.v1.Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            var orderFromDb = await _bll.Orders.FirstOrDefaultAsync(id);
            if (orderFromDb == null)
            {
                return NotFound();
            }

            orderFromDb = OrderMapper.ToBll(order);
            _bll.Orders.Update(orderFromDb);
            await _bll.SaveChangesAsync();
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrderExists(id))
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

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        /// <summary>
        /// Creates new order in the rest backend
        /// </summary>
        /// <param name="order">Supply order</param>
        /// <returns>New order</returns>
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.Order>> PostOrder([FromBody] App.Public.DTO.v1.OrderCont orderCont)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }

            var products = (await _bll.Products.GetAllAsync());
            var order = orderCont.Order;
            var orderBll = OrderMapper.ToBll(order);

            orderBll = _bll.Orders.Add(orderBll, orderCont.Products, products);
            await _bll.SaveChangesAsync();

            order.Id = orderBll.Id;
            
            return CreatedAtAction("GetOrder", new
            {
                id = order.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, order);
        }

        // DELETE: api/Order/5
        /// <summary>
        /// Delete order by id from the rest backend
        /// </summary>
        /// <param name="id">Supply order id</param>
        /// <returns>Nothing</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _bll.Orders.FirstOrDefaultAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _bll.Orders.Remove(order);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> OrderExists(Guid id)
        {
            return await _bll.Orders.ExistsAsync(id);
        }
    }
}
