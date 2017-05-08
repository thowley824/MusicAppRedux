 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicAppRedux.Models;
using Microsoft.AspNetCore.Authorization;

namespace MusicAppRedux.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly MusicDbContext _context;

        public ArtistsController(MusicDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var musicDbContext = _context.Artists;
            return View(await musicDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["Artist"] = _context.Artists.SingleOrDefault(g => g.ArtistID == id).Name;
            ViewData["Bio"] = _context.Artists.SingleOrDefault(g => g.ArtistID == id).Bio;
            var albumsByArtist = _context.Albums.Where(g => g.ArtistID == id).Include(g => g.Genre);
            return View(await albumsByArtist.ToListAsync());

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ArtistID, Name, Bio")] Artist artist)
        {
            if (ModelState.IsValid && NameExists(artist.Name) != true)
            {
                _context.Add(artist);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(artist);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists.SingleOrDefaultAsync(m => m.ArtistID == id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ArtistID, Name, Bio")] Artist artist)
        {
            if (id != artist.ArtistID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(artist.ArtistID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            return View(artist);
        }

        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.ArtistID == id);
        }

        private bool NameExists(string name)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return _context.Artists.Any(e => comparer.Equals(name, e.Name));
        }
    }
}