using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bmpt.Models
{
    public class Music
    {
        [Key]
        public int MusicId { get; set; }
        [Display(Name = "Nombre")]
        public string MusicName { get; set; }
    }
}
