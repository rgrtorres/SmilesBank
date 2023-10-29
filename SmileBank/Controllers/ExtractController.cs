using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmileBank.ContextDB;
using SmileBank.Interfaces;

namespace SmileBank.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class ExtractController : Controller
    {
        private readonly SmileBankContext context;

        public ExtractController(SmileBankContext context)
        {
            this.context = context;
        }

        [HttpGet("ListExtract")]
        public IActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            var query = context.Extract;
            if (startDate == null || endDate == null)
            {
                startDate = DateTime.Today.AddDays(-2);
                endDate = DateTime.Today;
                return Ok(query.Where(e => e.Date >= startDate && e.Date <= endDate).ToList());
            }
            else
            {
                return Ok(query.Where(e => e.Date >= startDate && e.Date <= endDate).ToList());
            }
        }

        [HttpPost("InsertExtract")]
        public IActionResult Insert(IExtract extract)
        {
            context.Add(extract);
            context.SaveChanges();

            return Ok(extract);
        }

        [HttpPost("UpdateExtract/{:id}")]
        public IActionResult Update(filterIExtract filter)
        {
            var found = context.Extract.Find(filter.id);

            if (found == null)
                return NotFound();

            found.Description = filter.description;
            found.Amount = filter.amount;
            found.Status = filter.status;

            context.Update(found);
            return Ok();
        }
    }
}
