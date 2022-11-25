using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Testing_Login;
using Testing_Login.Models;



namespace Testing_Login.Controllers
{
    public class BuysController : Controller
    {
        private readonly MYDbcontext _context;

        public BuysController(MYDbcontext context)
        {
            _context = context;
        }

        // GET: Buys
        public async Task<IActionResult> Index()
        {
            

            return View(await _context.Buy.ToListAsync());
        }
        public async Task<IActionResult> intheauction()
        {
            return View(await _context.Buy.ToListAsync());
        }

        // GET: Buys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Buy == null)
            {
                return NotFound();
            }

            var buy = await _context.Buy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buy == null)
            {
                return NotFound();
            }

            return View(buy);
        }

        // GET: Buys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Id,string car_Company,string car_model,int price,string car_condition,string Contact_number)
        {
            Buy b=new Buy();
            b.Id = Id;
            b.car_Company = car_Company;
            b.car_model = car_model;
            b.price = price;
            b.car_condition = car_condition;
            b.Contact_number=Contact_number;
            b.email= User.Identity.GetUserName();
            if (ModelState.IsValid)
            {

                _context.Add(b);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(intheauction));
            }
            return View(b);
        }

        // GET: Buys/Edit/5
        public async Task<IActionResult> Edit(int id,string email)
        {
            if (id == null || _context.Buy == null)
            {
                return NotFound();
            }

            var buy = await _context.Buy.FindAsync(id);
            if (buy == null)
            {
                return NotFound();
            }
            return View(buy);
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        

        // POST: Buys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        public async Task<IActionResult> Edit(int id, [Bind("Id,car_Company,car_model,price,car_condition,Contact_number,email")] Buy buy)
        {
            if (id != buy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuyExists(buy.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(intheauction));
            }
            return View(buy);
        }

        public async Task<IActionResult> Addtocart(int? id)
        {
            if (id == null || _context.Buy == null)
            {
                return NotFound();
            }

            var buy = await _context.Buy.FindAsync(id);
            if (buy == null)
            {
                return NotFound();
            }
            return View(buy);
        }


        [HttpPost]
        public async Task<IActionResult> Addtocart(int id)
        {
            
            Buy c = await _context.Buy.FirstOrDefaultAsync((m => m.Id == id));
            Cart b = new Cart();

         
            try
            {
                b.Contact_number = c.Contact_number;
                b.price=c.price;
                b.car_model=c.car_model;
                b.car_Company=c.car_Company;
                b.car_condition=c.car_condition;
                b.email = User.Identity.GetUserName();
                b.item_Id=id;
                _context.Cart.Add(b);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuyExists(c.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Carts", new {area="Views"});


        }

            // GET: Buys/Delete/5
            public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Buy == null)
            {
                return NotFound();
            }

            var buy = await _context.Buy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buy == null)
            {
                return NotFound();
            }

            return View(buy);
        }

        // POST: Buys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Buy == null)
            {
                return Problem("Entity set 'MYDbcontext.Buy'  is null.");
            }
            var buy = await _context.Buy.FindAsync(id);
            if (buy != null)
            {
                _context.Buy.Remove(buy);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(intheauction));
        }

        private bool BuyExists(int id)
        {
          return _context.Buy.Any(e => e.Id == id);
        }
    }
}
