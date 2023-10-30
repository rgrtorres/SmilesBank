﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("InsertExtract")]
        public IActionResult Insert(IExtract extract)
        {
            context.Add(extract);
            context.SaveChanges();

            return Ok(context.Extract.ToList());
        }

        [HttpPost("Update")]
        public IActionResult Update(IFilterExtract filter)
        {
            var found = context.Extract.Where(data => data.Id == filter.id).First();

            if (found == null)
                return NotFound();

            found.Description = filter.description;
            found.Amount = filter.amount;
            found.Status = filter.status;

            context.Update(found);
            context.SaveChanges();
            return Ok();
        }

        [HttpPost("Cancel")]
        public IActionResult Cancel(string id)
        {
            var found = context.Extract.Where(data => data.Id == Guid.Parse(id)).First();

            if (found == null)
                return NotFound();

            found.Status = "Cancelado";

            context.Update(found);
            context.SaveChanges();

            return Ok();
        }
    }
}
