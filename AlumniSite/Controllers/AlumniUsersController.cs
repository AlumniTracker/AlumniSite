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
using static AlumniSite.Data.Security;

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
        public async Task<IActionResult> Index() //replace this to be search, and then use general input?
        {
            var trackerContext = _context.AlumniUsers.Include(a => a.Address).Include(a => a.Employer);
            return View(await trackerContext.ToListAsync());
        }

        // GET: AlumniUsers/Details/5
        public async Task<IActionResult> Details(int? id) // be able to map random numbers to an id per session
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
            if(CheckInputs(alumniUser))
            {
                return View();
            }
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
        public static bool CheckInputs(AlumniUser alumniUser)
        {
            bool[] goodInput = new bool[18];
            goodInput[0] = GeneralInput(alumniUser.Username);
            goodInput[1] = GeneralInput(alumniUser.UserPw);
            // User Address Block
            goodInput[2] = GeneralInput(alumniUser.Address.Address1);
            goodInput[3] = GeneralInput(alumniUser.Address.City);
            goodInput[4] = GeneralInput(alumniUser.Address.State);
            goodInput[5] = GeneralInput(alumniUser.Address.Zip);
            goodInput[6] = PhoneInput(alumniUser.Address.Phone); //Our Phone Number
            //Employer Block
            goodInput[7] = GeneralInput(alumniUser.Employer.EmployerName);
            goodInput[8] = GeneralInput(alumniUser.Employer.Address.Address1);
            goodInput[9] = GeneralInput(alumniUser.Employer.Address.City);
            goodInput[10] = GeneralInput(alumniUser.Employer.Address.State);
            goodInput[11] = GeneralInput(alumniUser.Employer.Address.Zip);
            goodInput[12] = GeneralInput(alumniUser.Employer.Address.Phone);

            goodInput[13] = GeneralInput(alumniUser.EmployerName);
            goodInput[14] = GeneralInput(alumniUser.YearGraduated);
            goodInput[15] = GeneralInput(alumniUser.Degree);
            goodInput[16] = GeneralInput(alumniUser.Notes);

            //PhoneInput(alumniUser.PhoneNumber); //ASP NET Identity Phone Number

            
            
            
            if(!goodInput.Contains(false))
            {
                return true;
            }
            return false;
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
