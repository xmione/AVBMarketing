using AVBMarketing.Repositories;
using AVBMarketing.EF.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AVBMarketing.EF;

namespace AVBMarketing.API.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
     
        private readonly AVBMarketingContext _context;
        private readonly SqlMeetingRepository _repo;

        public MeetingsController(AVBMarketingContext context)
        {
            _context = context;
            _repo = new SqlMeetingRepository(context);
        }

        // GET: api/Meetings
        [HttpGet]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<IEnumerable<Meeting>>> GetMeetings()
        {
            var meetings = await _repo.GetAll();

            return Ok(meetings);

        }

        // GET: api/Meetings
        [HttpGet("{skip}/{take}")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<IEnumerable<Meeting>>> GetMeetings(int skip, int take)
        {
            var meetings = await _repo.GetAll(skip, take);
            return Ok(meetings);

        }

        // GET: api/GetMeetingsWithOverlaps
        [HttpGet("GetMeetingsWithOverlaps")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<List<Meeting>>> GetMeetingsWithOverlaps()
        {
            var meetings = await _repo.GetAll();
            var overlaps = await _repo.GetMeetingsWithOverlaps(meetings.ToList());

            return Ok(overlaps);

        }

        // GET: api/Meeting/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Meeting>> GetMeeting(int id)
        {
            var meeting = await _repo.Get(id);

            if (meeting == null)
            {
                return NotFound();
            }

            return meeting;
        }

        // PUT: api/Meetings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutMeeting(Meeting meeting)
        {
            _context.Entry(meeting).State = EntityState.Modified;

            try
            {
                await _repo.Update(meeting);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetingExists(meeting.Id))
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

        // POST: api/Meetings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Meeting>> PostMeeting(Meeting meeting)
        {
            try
            {
                meeting.Id = 0;
                await _repo.Add(meeting);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetMeeting", new { id = meeting.Id }, meeting);
        }

        // DELETE: api/Meetings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeeting(int id)
        {
            var meeting = await _context.Meetings.FindAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }

            _context.Meetings.Remove(meeting);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MeetingExists(int id)
        {
            return _context.Meetings.Any(e => e.Id == id);
        }
    }
}
