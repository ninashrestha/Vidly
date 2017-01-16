using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class NewMovie
    {
        public IEnumerable<Genre> Genre { get; set; }
        public Movies Movie { get; set; }
    }
}