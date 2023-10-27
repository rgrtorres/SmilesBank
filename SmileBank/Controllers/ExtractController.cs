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

        public static List<IExtract> Extract = new()
        {
            new (Guid.NewGuid(), "Salário", 2857.00, false, "Válido"),
            new (Guid.NewGuid(), "Super Mercado Guassu", 128.32, false, "Válido"),
        };

        public ExtractController()
        {
            this.context = context;
        }

        [HttpGet("ListExtract")]
        public IActionResult Index()
        {
            return Ok(context.Extract.ToList());
        }

        [HttpPost("InsertExtract")]
        public IActionResult Insert(IExtract extract)
        {
            context.Add(extract);

            return Ok(Extract);
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
