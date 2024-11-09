using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication32.Models
{
    public class MoviesViewModel
    {
        public IEnumerable<Movies> listofmovies { get; set; }
        public Movies newmovie { get; set; } = new Movies();

    }
}