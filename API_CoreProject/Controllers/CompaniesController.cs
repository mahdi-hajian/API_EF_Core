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
    //[EnableCors("SiteCorsPolicy")]
    [Produces("application/json")]
    [Route("api/Companies")]
    public class CompaniesController : Controller
    {
        private readonly Mcontext _context;

        public CompaniesController(Mcontext context)
        {
            _context = context;
        }

        // GET: api/Companies
        [HttpGet]
        public IEnumerable<Company> GetCompanies()
        {
            return _context.Companies;
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var company = await _context.Companies.SingleOrDefaultAsync(m => m.ID == id);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // PUT: api/Companies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany([FromRoute] long id, [FromBody] Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!CompanyExists(id))
            {
                return NotFound();
            }

            await _context.Companies.Where(c => c.ID == id).LoadAsync();

            foreach (var item in _context.Companies.Local.ToObservableCollection())
            {
                item.Description = company.Description;
                item.Name = company.Name;
                item.SetTime();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCompany", new { id }, company);
        }

        // POST: api/Companies
        [HttpPost]
        public async Task<IActionResult> PostCompany([FromBody] Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompany", new { id = company.ID }, company);
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var company = await _context.Companies.SingleOrDefaultAsync(m => m.ID == id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return Ok(company);
        }

        private bool CompanyExists(long id)
        {
            return _context.Companies.Any(e => e.ID == id);
        }
    }
}