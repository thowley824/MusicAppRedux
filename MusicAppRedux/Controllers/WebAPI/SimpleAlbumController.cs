using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicAppRedux.Models;

namespace MusicAppRedux.Controllers.WebAPI
{ 
    [Route("api/SimpleAlbum")]
    public class SimpleAlbumController : Controller
    {
        private readonly MusicDbContext _context;

        public SimpleAlbumController(MusicDbContext context)
        {
            _context = context;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<Album> Get()
        {
            return _context.Albums.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Album Get(int id)
        {
            return _context.Albums.SingleOrDefault(p => p.AlbumID == id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Album album)
        {
            _context.Add(album);
            _context.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Album album)
        {
            _context.Albums.Update(album);
            _context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var album = _context.Albums.Single(p => p.AlbumID == id);
            _context.Albums.Remove(album);
            _context.SaveChanges();
        }
    }
}
