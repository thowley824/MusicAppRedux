using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MusicAppRedux.Models.AccountViewModels
{
    public class UserProfileViewModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Favorite Genre")]
        public int UserGenreID { get; set; }
        
    }
}
