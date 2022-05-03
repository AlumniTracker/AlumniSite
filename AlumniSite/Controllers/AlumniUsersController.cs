#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlumniSite.Contexts;
using AlumniSite.Models;

namespace AlumniSite.Controllers
{
    public class AlumniUsersController : Controller
    {
        private readonly TrackerContext _context;

        public AlumniUsersController(TrackerContext context)
        {
            _context = context;
        }

        // GET: AlumniUsers
        public async Task<IActionResult> Index()
        {
            var trackerContext = _context.AlumniUsers.Include(a => a.Address).Include(a => a.Employer);
            return View(await trackerContext.ToListAsync());
        }

        // GET: AlumniUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumniUser = await _context.AlumniUsers
                .Include(a => a.Address)
                .Include(a => a.Employer)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (alumniUser == null)
            {
                return NotFound();
            }

            return View(alumniUser);
        }

        // GET: AlumniUsers/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId");
            ViewData["EmployerId"] = new SelectList(_context.Employers, "EmployerId", "EmployerId");
            return View();
        }

        // POST: AlumniUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Username,UserPw,IsAdmin,AddressId,EmployerId,EmployerName,YearGraduated,Degree,Notes,AdminType,DateModified")] AlumniUser alumniUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alumniUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", alumniUser.AddressId);
            ViewData["EmployerId"] = new SelectList(_context.Employers, "EmployerId", "EmployerId", alumniUser.EmployerId);
            return View(alumniUser);
        }

        // GET: AlumniUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumniUser = await _context.AlumniUsers.FindAsync(id);
            if (alumniUser == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", alumniUser.AddressId);
            ViewData["EmployerId"] = new SelectList(_context.Employers, "EmployerId", "EmployerId", alumniUser.EmployerId);
            return View(alumniUser);
        }

        // POST: AlumniUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Username,UserPw,IsAdmin,AddressId,EmployerId,EmployerName,YearGraduated,Degree,Notes,AdminType,DateModified")] AlumniUser alumniUser)
        {
            if (id != alumniUser.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alumniUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumniUserExists(alumniUser.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", alumniUser.AddressId);
            ViewData["EmployerId"] = new SelectList(_context.Employers, "EmployerId", "EmployerId", alumniUser.EmployerId);
            return View(alumniUser);
        }

        // GET: AlumniUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumniUser = await _context.AlumniUsers
                .Include(a => a.Address)
                .Include(a => a.Employer)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (alumniUser == null)
            {
                return NotFound();
            }

            return View(alumniUser);
        }

        // POST: AlumniUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alumniUser = await _context.AlumniUsers.FindAsync(id);
            _context.AlumniUsers.Remove(alumniUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumniUserExists(int id)
        {
            return _context.AlumniUsers.Any(e => e.UserId == id);
        }
    }
}
