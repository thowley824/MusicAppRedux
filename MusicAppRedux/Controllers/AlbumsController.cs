using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicAppRedux.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MusicAppRedux.Controllers
{
    
    public class AlbumsController : Controller
    {
        private readonly MusicDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AlbumsController(MusicDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Albums
        public async Task<IActionResult> PartIndex()
        {
            if (User.Identity.IsAuthenticated)
            {
               
                var thisUser = await _userManager.GetUserAsync(User);
                int userID = thisUser.UserFavoriteGenreID;
                var thisMusicDbContext = _context.Albums.Where(a => a.GenreID ==thisUser.UserFavoriteGenreID).Include(a => a.Artist).Include(a => a.Genre);
                ViewData["ChosenGenre"] = _context.Genres.SingleOrDefault(g => g.GenreID == userID).Name;
                return View(await thisMusicDbContext.ToListAsync());
            }
            else
            {
               return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Index()
        {
            var musicDbContext = _context.Albums.Include(a => a.Artist).Include(a => a.Genre);
            return View(await musicDbContext.ToListAsync());
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumContext =  _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre);
            var album = await albumContext
                .SingleOrDefaultAsync(m => m.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Details(int id, int AvgRate)
        {
            var album = _context.Albums.FirstOrDefault(a => a.AlbumID == id);

            _context.Rates.Add(new Rate { AlbumId = id, Value = AvgRate});
            await _context.SaveChangesAsync();

            var ratings = _context.Rates.Where(r => r.AlbumId == id).ToList();
            var sumOfRatings = 0f;

            for (int i = 0; i < ratings.Count; i++)
            {
                sumOfRatings = sumOfRatings + (float)ratings[i].Value;
            }

            sumOfRatings = sumOfRatings/(float)ratings.Count;
            album.AvgRate = (float)Math.Round(sumOfRatings, 1);

            _context.Update(album);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Albums/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["ArtistID"] = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Name");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("AlbumID,Title,ArtistID,GenreID,Likes")] Album album)
        { 
            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ArtistID"] = new SelectList(_context.Artists, "ArtistID", "Name", album.ArtistID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.SingleOrDefaultAsync(m => m.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }
            ViewData["ArtistID"] = new SelectList(_context.Artists, "ArtistID", "Name", album.ArtistID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("AlbumID,Title,ArtistID,GenreID,Likes")] Album album)
        {
            if (id != album.AlbumID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.AlbumID))
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
            ViewData["ArtistID"] = new SelectList(_context.Artists, "ArtistID", "Name", album.ArtistID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .SingleOrDefaultAsync(m => m.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albums.SingleOrDefaultAsync(m => m.AlbumID == id);
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.AlbumID == id);
        }
    }
}
