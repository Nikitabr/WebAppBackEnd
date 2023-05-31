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
    public class FeedbacksController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public FeedbacksController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Feedback
        /// <summary>
        /// Get all feedbacks from the rest backend
        /// </summary>
        /// <returns>All feedbacks</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.Feedback>), 200 )]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Feedback>>> GetFeedbacks()
        {
            var res = (await _bll.Feedbacks.GetAllAsync())
                .Select(FeedbackMapper.ToPublic).ToList();
            
            return res;
        }

        // GET: api/Feedback/5
        /// <summary>
        /// Get feedback by id from the rest backend
        /// </summary>
        /// <param name="id">Supply feedback id</param>
        /// <returns>Feedback by id</returns>
        [Produces( "application/json" )]
        [Consumes( "application/json" )]
        [ProducesResponseType( typeof(App.Public.DTO.v1.Feedback), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.Feedback>> GetFeedback(Guid id)
        {
            var feedback = await _bll.Feedbacks.FirstOrDefaultAsync(id);

            if (feedback == null)
            {
                return NotFound();
            }

            return FeedbackMapper.ToPublic(feedback);
        }

        // PUT: api/Feedback/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Insert new data to feedback by id, edit feedback in the rest backend
        /// </summary>
        /// <param name="id">Supply feedback id</param>
        /// <param name="feedback">Supply feedback</param>
        /// <returns>Updated feedback</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedback(Guid id, App.Public.DTO.v1.Feedback feedback)
        {
            if (id != feedback.Id)
            {
                return BadRequest();
            }

            var feedbackFromDb = await _bll.Feedbacks.FirstOrDefaultAsync(id);
            if (feedbackFromDb == null)
            {
                return NotFound();
            }

            feedbackFromDb = FeedbackMapper.ToBll(feedback);
            _bll.Feedbacks.Update(feedbackFromDb);
            await _bll.SaveChangesAsync();

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await FeedbackExists(id))
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

        // POST: api/Feedback
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create new feedback in the rest backend
        /// </summary>
        /// <param name="feedback">Supply feedback</param>
        /// <returns>New feedback</returns>
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.Feedback>> PostFeedback([FromBody] App.Public.DTO.v1.Feedback feedback)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }
            
            var feedbackBll = FeedbackMapper.ToBll(feedback);
            
            _bll.Feedbacks.Add(feedbackBll);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetFeedback", new
            {
                id = feedback.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, feedback);
        }

        // DELETE: api/Feedback/5
        /// <summary>
        /// Delete feedback by id from the rest backend
        /// </summary>
        /// <param name="id">Supply feedback id</param>
        /// <returns>Nothing</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(Guid id)
        {
            var feedback = await _bll.Feedbacks.FirstOrDefaultAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            _bll.Feedbacks.Remove(feedback);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> FeedbackExists(Guid id)
        {
            return await _bll.Feedbacks.ExistsAsync(id);
        }
    }
}
