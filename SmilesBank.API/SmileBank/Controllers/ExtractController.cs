using Microsoft.AspNetCore.Mvc;
using SmileBank.ContextDB;
using SmileBank.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            //var query = context.Extract;
            //if (startDate == null || endDate == null)
            //{
            //    startDate = DateTime.Today.AddDays(-2);
            //    endDate = DateTime.Today;
            //    return Ok(query.Where(e => e.Date >= startDate && e.Date <= endDate).ToList());
            //}
            //else
            //{
            //    return Ok(query.Where(e => e.Date >= startDate && e.Date <= endDate).ToList());
            //}

            return Ok(context.Extract.ToList());
        }

        [HttpPost("ListExtractByDate")]
        public IActionResult ListByDate(IFilterDate filter)
        {
            if (filter.startDate == null || filter.endDate == null)
            {
                return BadRequest("Datas de início e/ou fim não podem ser nulas.");
            }

            var query = context.Extract;

            var result = query.Where(e => e.Date >= filter.startDate && e.Date <= filter.endDate).ToList();

            return Ok(result);
        }


        [HttpPost("InsertExtract")]
        public IActionResult Insert(IExtract extract)
        {
            context.Add(extract);
            context.SaveChanges();

            return Ok(context.Extract.ToList());
        }

        [HttpPost("Update")]
        public IActionResult Update(IUpdate filter)
        {
            var found = context.Extract.Where(data => data.Id == filter.id).First();

            if (found == null)
                return NotFound();

            found.Description = filter.description;
            found.Amount = filter.amount;

            context.Update(found);
            context.SaveChanges();
            return Ok();
        }

        [HttpPost("Cancel")]
        public IActionResult Cancel(ICancel filter)
        {
            var found = context.Extract.Where(data => data.Id == filter.id).First();

            if (found == null)
                return NotFound();

            found.Status = "Cancelado";

            context.Update(found);
            context.SaveChanges();

            return Ok();
        }

        private static readonly Random random = new Random();
        [HttpGet("automatic")]
        public IActionResult automatic()
        {
            var data = new IExtract("System", random.Next(1, 1000), false, "Válido");


            context.Add(data);
            context.SaveChanges();

            return Ok(context.Extract.ToList());
        }
    }
}
