using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movies
    {
        public int Id { get; set; }
       public string Name { get; set; }
    
        public Genre genre { get; set; }
        [Display(Name = "Genre")]
        [Required]
        public byte GenreId { get; set; }
        [Display(Name = "Date Added")]
       public DateTime DateAdded { get; set; }
        [Display(Name= "Released Date")]
       public DateTime ReleaseDate { get; set; }
      public byte NumberInStock { get; set; }


    }
}