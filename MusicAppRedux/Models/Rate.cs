using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicAppRedux.Models
{
    public class Rate
    {
       public int RateId { get; set; }
       public int Value { get; set; }

       public  int AlbumId { get; set; }
       public Album album { get; set; }
    }
}
