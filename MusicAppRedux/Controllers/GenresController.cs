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
    public class GenresController : Controller
    {
        private readonly MusicDbContext _context;


        public GenresController(MusicDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Genres.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();  
        }

        public async Task<IActionResult> Details (int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            ViewData["Genre"] = _context.Genres.SingleOrDefault(g => g.GenreID == id).Name;
            var albumsInGenre = _context.Albums.Where(g => g.GenreID == id).Include(g => g.Artist);
            return View(await albumsInGenre.ToListAsync());

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("GenreID, Name")] Genre genre)
        {
            if(ModelState.IsValid && NameExists(genre.Name) != true)
            {
                _context.Add(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres.SingleOrDefaultAsync(m => m.GenreID == id);
            if(genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("GenreID, Name")] Genre genre)
        {
            if(id != genre.GenreID)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(genre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(genre.GenreID))
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

            return View(genre);
        }

        private bool GenreExists(int id)
        {
            return _context.Genres.Any(e => e.GenreID == id);
        }

        private bool NameExists(string name)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return _context.Genres.Any(e => comparer.Equals(name, e.Name));
        }
    }
}
