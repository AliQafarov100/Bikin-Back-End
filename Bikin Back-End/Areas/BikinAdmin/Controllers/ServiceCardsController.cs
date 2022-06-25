using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bikin_Back_End.DAL;
using Bikin_Back_End.Models;

namespace Bikin_Back_End.Areas.BikinAdmin.Controllers
{
    [Area("BikinAdmin")]
    public class ServiceCardsController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceCardsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: BikinAdmin/ServiceCards
        public async Task<IActionResult> Index()
        {
            return View(await _context.ServiceCards.ToListAsync());
        }

        // GET: BikinAdmin/ServiceCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceCard = await _context.ServiceCards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceCard == null)
            {
                return NotFound();
            }

            return View(serviceCard);
        }

        // GET: BikinAdmin/ServiceCards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BikinAdmin/ServiceCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Icon,Title,Description")] ServiceCard serviceCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceCard);
        }

        // GET: BikinAdmin/ServiceCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceCard = await _context.ServiceCards.FindAsync(id);
            if (serviceCard == null)
            {
                return NotFound();
            }
            return View(serviceCard);
        }

        // POST: BikinAdmin/ServiceCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Icon,Title,Description")] ServiceCard serviceCard)
        {
            if (id != serviceCard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceCardExists(serviceCard.Id))
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
            return View(serviceCard);
        }

        // GET: BikinAdmin/ServiceCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceCard = await _context.ServiceCards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceCard == null)
            {
                return NotFound();
            }

            return View(serviceCard);
        }

        // POST: BikinAdmin/ServiceCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceCard = await _context.ServiceCards.FindAsync(id);
            _context.ServiceCards.Remove(serviceCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceCardExists(int id)
        {
            return _context.ServiceCards.Any(e => e.Id == id);
        }
    }
}
