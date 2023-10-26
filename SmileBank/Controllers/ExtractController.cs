using Microsoft.AspNetCore.Mvc;
using SmileBank.Interfaces;

namespace SmileBank.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class ExtractController : Controller
    {
        public static List<IExtract> Extract = new()
        {
            new ("Salário", 2857.00, false, "Válido"),
            new ("Super Mercado Guassu", 128.32, false, "Válido"),
        };

        [HttpGet("ListExtract")]
        public IActionResult Index()
        {
            return Ok(Extract);
        }

        [HttpPost("InsertExtract")]
        public IActionResult Insert(IExtract extract)
        {
            Extract.Add(extract);
            return Ok(Extract);
        }
    }
}
