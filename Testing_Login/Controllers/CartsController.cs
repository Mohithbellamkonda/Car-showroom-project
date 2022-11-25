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
    public class CartsController : Controller
    {
        private readonly MYDbcontext _context;

        public CartsController(MYDbcontext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
              return View(await _context.Cart.ToListAsync());
        }
        public async Task<IActionResult> NO_items()
        {
            return View();
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cart == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        public async Task<IActionResult> Wanttobuy(int? id)
        {   
           
            
            var C = await _context.Cart .FirstOrDefaultAsync(m => m.Id == id);
            int i = C.item_Id;
            Buy b = new Buy();
            b = await _context.Buy.FindAsync(i);
           

            if (b == null)
            {
                _context.Cart.Remove(C);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(NO_items));
            }
            C.price = b.price;
            C.car_condition = b.car_condition;
            C.Contact_number = b.Contact_number;
            _context.Cart.Update(C);
            await _context.SaveChangesAsync();
            return View(C);


        }
        [HttpPost]
        public async Task<IActionResult> Wanttobuy(int id)
        {

            var c = await _context.Cart.FirstOrDefaultAsync(m => m.Id == id);
            int i=c.item_Id;
            Buy b = new Buy();
            b = await _context.Buy.FindAsync(i);
            Sold sold = new Sold();
            Brought brought = new Brought();
          
            sold.car_model = c.car_model;
            sold.price=c.price;
            sold.car_Company=c.car_Company;
            sold.Contact_number=c.Contact_number;
            sold.email=b.email;
            sold.car_condition=c.car_condition;



            brought.car_model = b.car_model;
            brought.price = b.price;
            brought.car_Company = b.car_Company;
            brought.Contact_number = b.Contact_number;
            brought.email = c.email;
            brought.car_condition = b.car_condition;

            _context.Sold.Add(sold);
            _context.Brought.Add(brought);

            _context.Cart.Remove(c);
            _context.Buy.Remove(b);

            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "Broughts",new {Area="Views"});



        }














        // GET: Carts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,car_Company,car_model,price,car_condition,Contact_number,email")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cart);

        }



        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cart == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,car_Company,car_model,price,car_condition,Contact_number,email")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
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
            return View(cart);
        }


        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cart == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cart == null)
            {
                return Problem("Entity set 'MYDbcontext.Cart'  is null.");
            }
            var cart = await _context.Cart.FindAsync(id);
            if (cart != null)
            {
                _context.Cart.Remove(cart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
          return _context.Cart.Any(e => e.Id == id);
        }
    }
}
