using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bikin_Back_End.DAL;
using Bikin_Back_End.Models;
using Bikin_Back_End.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bikin_Back_End.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<ServiceCard> cards = await _context.ServiceCards.ToListAsync();
            List<AboutCards> aboutCards = await _context.AboutCards.ToListAsync();
            List<Sponsor> sponsors = await _context.Sponsors.ToListAsync();
            MainPage page = await _context.MainPages.FirstOrDefaultAsync();
            HomeVM model = new HomeVM
            {
                Cards = cards,
                Sponsors = sponsors,
                AboutCards = aboutCards,
                Page = page
            };
            return View(model);
        }
    }
}
