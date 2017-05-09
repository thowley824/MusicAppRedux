using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicAppRedux.Models
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly MusicDbContext _context;

        public AlbumRepository(MusicDbContext context)
        {
            _context = context;
        }


        public List<Album> GetAlbumList()
        {
            return _context.Albums.ToList();
        }

        public Album GetAlbum(int id)
        {
            return _context.Albums.SingleOrDefault(p => p.AlbumID == id);
        }


        public void Add(Album album)
        {
            _context.Add(album);
            _context.SaveChanges();
        }

        public Album UpdateAlbum(Album album)
        {
            _context.Albums.Update(album);
            _context.SaveChanges();
            return album;
        }

    }


}

