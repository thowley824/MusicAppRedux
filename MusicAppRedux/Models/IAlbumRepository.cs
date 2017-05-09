using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicAppRedux.Models
{
    interface IAlbumRepository
    {
        void Add(Album album);
        List<Album> GetAlbumList();
        Album GetAlbum(int id);

        Album UpdateAlbum(Album album);
    }
}
