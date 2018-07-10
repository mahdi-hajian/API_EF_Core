using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_CoreProject.Models;
using API_CoreProject.Models.CRUD_Angular;

namespace API_CoreProject.Controllers
{
    [Produces("application/json")]
    [Route("api/Technologies")]
    public class TechnologiesController : Controller
    {
        private readonly Mcontext _context;

        public TechnologiesController(Mcontext context)
        {
            _context = context;
        }

        // GET: api/Technologies
        [HttpGet]
        public IEnumerable<Technology> GetTechnologies()
        {
            return _context.Technologies;
        }

        // GET: api/Technologies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTechnology([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var technology = await _context.Technologies.SingleOrDefaultAsync(m => m.ID == id);

            if (technology == null)
            {
                return NotFound();
            }

            return Ok(technology);
        }

        // PUT: api/Technologies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTechnology([FromRoute] long id, [FromBody] Technology technology)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Companies.Where(c => c.ID == technology.CompanyID).FirstOrDefault() == null)
            {
                return NotFound();
            }
            if (!TechnologyExists(id))
            {
                return NotFound();
            }

            await _context.Technologies.Where(c => c.ID == id).LoadAsync();


            foreach (var item in _context.Technologies.Local.ToObservableCollection())
            {
                item.Name = technology.Name;
                item.Description = technology.Description;
                item.CompanyID = technology.CompanyID;
                item.SetTime();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TechnologyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTechnology", new { id }, technology);
        }

        // POST: api/Technologies
        [HttpPost]
        public async Task<IActionResult> PostTechnology([FromBody] Technology technology)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Technologies.Add(technology);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTechnology", new { id = technology.ID }, technology);
        }

        // DELETE: api/Technologies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTechnology([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var technology = await _context.Technologies.SingleOrDefaultAsync(m => m.ID == id);
            if (technology == null)
            {
                return NotFound();
            }

            _context.Technologies.Remove(technology);
            await _context.SaveChangesAsync();

            return Ok(technology);
        }

        private bool TechnologyExists(long id)
        {
            return _context.Technologies.Any(e => e.ID == id);
        }
    }
}