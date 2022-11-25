using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Testing_Login;
using Testing_Login.Models;

namespace Testing_Login.Controllers
{
    public class BroughtsController : Controller
    {
        private readonly MYDbcontext _context;

        public BroughtsController(MYDbcontext context)
        {
            _context = context;
        }

        // GET: Broughts
        public async Task<IActionResult> Index()
        {
              return View(await _context.Brought.ToListAsync());
        }

        // GET: Broughts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Brought == null)
            {
                return NotFound();
            }

            var brought = await _context.Brought
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brought == null)
            {
                return NotFound();
            }

            return View(brought);
        }

        // GET: Broughts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Broughts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,car_Company,car_model,price,car_condition,Contact_number,email")] Brought brought)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brought);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brought);
        }

        // GET: Broughts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Brought == null)
            {
                return NotFound();
            }

            var brought = await _context.Brought.FindAsync(id);
            if (brought == null)
            {
                return NotFound();
            }
            return View(brought);
        }

        // POST: Broughts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,car_Company,car_model,price,car_condition,Contact_number,email")] Brought brought)
        {
            if (id != brought.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brought);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BroughtExists(brought.Id))
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
            return View(brought);
        }

        // GET: Broughts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Brought == null)
            {
                return NotFound();
            }

            var brought = await _context.Brought
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brought == null)
            {
                return NotFound();
            }

            return View(brought);
        }

        // POST: Broughts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Brought == null)
            {
                return Problem("Entity set 'MYDbcontext.Brought'  is null.");
            }
            var brought = await _context.Brought.FindAsync(id);
            if (brought != null)
            {
                _context.Brought.Remove(brought);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BroughtExists(int id)
        {
          return _context.Brought.Any(e => e.Id == id);
        }
    }
}
