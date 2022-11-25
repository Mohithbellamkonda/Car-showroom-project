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
    public class SoldsController : Controller
    {
        private readonly MYDbcontext _context;

        public SoldsController(MYDbcontext context)
        {
            _context = context;
        }

        // GET: Solds
        public async Task<IActionResult> Index()
        {
              return View(await _context.Sold.ToListAsync());
        }

        // GET: Solds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sold == null)
            {
                return NotFound();
            }

            var sold = await _context.Sold
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sold == null)
            {
                return NotFound();
            }

            return View(sold);
        }

        // GET: Solds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Solds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,car_Company,car_model,price,car_condition,Contact_number,email")] Sold sold)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sold);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sold);
        }

        // GET: Solds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sold == null)
            {
                return NotFound();
            }

            var sold = await _context.Sold.FindAsync(id);
            if (sold == null)
            {
                return NotFound();
            }
            return View(sold);
        }

        // POST: Solds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,car_Company,car_model,price,car_condition,Contact_number,email")] Sold sold)
        {
            if (id != sold.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sold);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoldExists(sold.Id))
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
            return View(sold);
        }

        // GET: Solds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sold == null)
            {
                return NotFound();
            }

            var sold = await _context.Sold
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sold == null)
            {
                return NotFound();
            }

            return View(sold);
        }

        // POST: Solds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sold == null)
            {
                return Problem("Entity set 'MYDbcontext.Sold'  is null.");
            }
            var sold = await _context.Sold.FindAsync(id);
            if (sold != null)
            {
                _context.Sold.Remove(sold);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoldExists(int id)
        {
          return _context.Sold.Any(e => e.Id == id);
        }
    }
}
