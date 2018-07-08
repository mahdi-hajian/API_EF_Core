using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_CoreProject.Models.CRUD_Angular;
using API_CoreProject.Models;
using System.Threading;

namespace API_CoreProject.Controllers
{
    //[EnableCors("SiteCorsPolicy")]
    [Route("api/Companies")] 
    public class CompaniesController : Controller
    {
        public Mcontext Ocontext()
        {
            Mcontext mcontext = null;
            try
            {
                mcontext = new Mcontext();
            }
            catch (Exception)
            {
            }
            return mcontext;
        }
        // GET: api/Companies
        [HttpGet]
        public IEnumerable<Company> GetCompanies()
        {
            return Ocontext().Companies;
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany([FromRoute] long id)
        {
            List<Company> mahdi = new List<Company>();

            Company company = null;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                company = await Ocontext().Companies.SingleOrDefaultAsync(m => m.ID == id);

                if (company == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
            }

            return Ok(company);
        }

        // PUT: api/Companies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany([FromRoute] long id, [FromBody] Company company)
        {
            Mcontext mcontext = null;
            try
            {
                mcontext = new Mcontext();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (mcontext.Companies.Where(c => c.ID == id).FirstOrDefault() == null)
                {
                    return NotFound();
                }
                mcontext.Companies.Where(c => c.ID == id).Load();
                foreach (var item in mcontext.Companies.Local.ToObservableCollection())
                {
                    item.Name = company.Name;
                    item.Description = company.Description;
                }

                try
                {
                    await mcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }
            catch (Exception)
            {
            }

            return CreatedAtAction("GetCompany", new { id = company.ID }, company);
        }

        // POST: api/Companies
        [HttpPost]
        public async Task<IActionResult> PostCompany([FromBody] Company company)
        {
            Mcontext mcontext = null;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                mcontext = new Mcontext();

                mcontext.Companies.Add(company);
                await mcontext.SaveChangesAsync();
            }
            catch (Exception)
            {
            }

            return CreatedAtAction("GetCompany", new { id = company.ID }, company);
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany([FromRoute] long id)
        {
            Mcontext mcontext = null;
            Company company = null;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                mcontext = new Mcontext();
                company = await mcontext.Companies.Where(c => c.ID == id).SingleOrDefaultAsync();
                if (company == null)
                {
                    return NotFound();
                }

                mcontext.Companies.Remove(company);
                await mcontext.SaveChangesAsync();
            }
            catch (Exception)
            {
            }

            return Ok(company);
        }

        private bool CompanyExists(long id)
        {
            return Ocontext().Companies.Any(e => e.ID == id);
        }
    }
}