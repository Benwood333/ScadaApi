using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScadaApi.Models;

namespace ScadaApi.Controllers
{
    [ApiController]
    [ResponseCache(CacheProfileName = "Default5")]
    public class RtuController : ControllerBase
    {

        private readonly RtuDbContext _context;

        public RtuController(RtuDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [HttpHead]
        [Route("GetRtus")]
        public async Task<ActionResult<IEnumerable<Rtu>>> GetRtus()
        {
            var rtus = await _context.Rtus.ToListAsync();
            return rtus.Count > 0 ? Ok(rtus) : NoContent();
        }

        [HttpGet("GetNamePointsByRtu/{name}")]
        [HttpHead("GetNamePointsByRtu/{name}")]
        public async Task<ActionResult<IEnumerable<string>>> GetNamePointsRtu([FromRoute] string name)
        {
            var rtu = await _context.Rtus.FirstOrDefaultAsync(r => r.Name == name);

            if (rtu == null || rtu.Points == null || rtu.Points.Count == 0)
            {
                return NoContent();
            }

            var pNames = await _context.Points
                .Where(p => p.RtuId == rtu.RtuId)
                .Select(p => p.Name)
                .ToListAsync();

            return Ok(pNames);
        }

        [HttpGet("GetNumPointsByRtu/{name}")]
        [HttpHead("GetNumPointsByRtu/{name}")]
        public async Task<ActionResult<int>> GetNumPointsRtu([FromRoute] string name)
        {
            var rtu = await _context.Rtus.FirstOrDefaultAsync(r => r.Name == name);

            if (rtu == null || rtu.Points == null)
            {
                return NoContent();
            }

            return Ok(rtu.Points.Count);
        }

        [HttpGet("GetValuePoint/{rtuName}/{pointName}")]
        [HttpHead("GetValuePoint/{rtuName}/{pointName}")]
        public async Task<ActionResult<int>> GetValPointsRtu([FromRoute] string rtuName, [FromRoute] string pointName)
        {
            var rtu = await _context.Rtus.FirstOrDefaultAsync(r => r.Name == rtuName);

            if (rtu == null)
            {
                return NoContent();
            }

            var point = await _context.Points.FirstOrDefaultAsync(p => p.Name == pointName && p.RtuId == rtu.RtuId);
            if (point == null)
            {
                return NoContent();
            }


            return Ok(point.Value);
        }

        [HttpGet("GetTimeStampPoint/{rtuName}/{pointName}")]
        [HttpHead("GetTimeStampPoint/{rtuName}/{pointName}")]
        public async Task<ActionResult<DateTime>> GetTimeStampPointsRtu([FromRoute] string rtuName, [FromRoute] string pointName)
        {
            var rtu = await _context.Rtus.FirstOrDefaultAsync(r => r.Name == rtuName);

            if (rtu == null)
            {
                return NoContent();
            }

            var point = await _context.Points.FirstOrDefaultAsync(p => p.Name == pointName && p.RtuId == rtu.RtuId);
            if (point == null)
            {
                return NoContent();
            }

            return Ok(point.TimeStamp);
        }
    }
}
