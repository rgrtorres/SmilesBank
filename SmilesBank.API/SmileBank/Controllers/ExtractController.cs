using Microsoft.AspNetCore.Mvc;
using SmileBank.ContextDB;
using SmileBank.Interfaces;
using System.Globalization;
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

        [HttpPost("ListExtract")]
        public IActionResult Index(IFilterDate filter)
        {
            var query = context.Extract;

            object result = "";
            DateTime date = DateTime.Now.Date;
            CultureInfo culturaBrasileira = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = culturaBrasileira;
            CultureInfo.DefaultThreadCurrentUICulture = culturaBrasileira;

            if (filter.startDate.Date == date && filter.endDate.Date == date)
            {
                filter.startDate = DateTime.Today.AddDays(-2);
                filter.endDate = DateTime.Today.Date;
                result = query.Where(e => e.Date >= filter.startDate.Date && e.Date <= filter.endDate.Date.AddDays(1)).ToList();
            } else
            {
                result = query.Where(e => e.Date >= filter.startDate.Date && e.Date <= filter.endDate.Date).ToList();
            }

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
