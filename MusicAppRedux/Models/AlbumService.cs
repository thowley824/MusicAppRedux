using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicAppRedux.Controllers.WebAPI;

namespace MusicAppRedux.Models
{
    public class AlbumService : IAlbumRepository
    {
        private SimpleAlbumController _albumService;

        public AlbumService(SimpleAlbumController albumService)
        {
            _albumService = albumService; 
        }

        public void Add(Album album)
        {
            _albumService.Post(album);
        }

        public Album GetAlbum(int id)
        {
            return _albumService.Get(id);
        }

        public List<Album> GetAlbumList()
        {
            return _albumService.Get().ToList();
        }

        public Album UpdateAlbum(Album album)
        {
            throw new NotImplementedException();
        }
    }
}
